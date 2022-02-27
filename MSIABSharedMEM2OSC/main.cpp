#define _AFXDLL
#define _USE_32BIT_TIME_T

#include <stdio.h>
#include "MSIAB.h"
#include "stdafx.h"
#include "MAHMSharedMemory.h"
#include <iostream>

void main()
{
	CString m_MSIABStatus;

	MSIAB afterburner;

	//MSI Afterburner is installed
	if (!afterburner.GetMSIABInstallPath().IsEmpty())
	{
		//if we're not connected to MSI Afterburner hardware monitoring shared memory yet - do it now

		if (!afterburner.GetMapAddress())
			afterburner.Connect();

		if (afterburner.GetMapAddress())
			//if we're connected to shared memory, we must check if it is valid or not and reconnect if necessary
		{
			LPMAHM_SHARED_MEMORY_HEADER lpHeader = (LPMAHM_SHARED_MEMORY_HEADER)afterburner.GetMapAddress();

			if (lpHeader->dwSignature == 0xDEAD)
				//if the memory is marked as dead (e.g. MSI Afterburner was unloaded), we should disconnect from it and
				//try to connect again
				afterburner.Connect();
		}

		if (afterburner.GetMapAddress())
		{
			LPMAHM_SHARED_MEMORY_HEADER	lpHeader = (LPMAHM_SHARED_MEMORY_HEADER)afterburner.GetMapAddress();

			if (lpHeader->dwSignature == 'MAHM')
				//check if we're connected to valid memory
			{
				CTime time(lpHeader->time);
				CString strBuf = time.Format("%d-%m-%Y %H:%M:%S");
				//format data polling time

				DWORD dwSources = lpHeader->dwNumEntries;
				//get number of data sources

				m_MSIABStatus.Format(_T("Connected to MSI Afterburner Hardware Monitoring shared memory v%d.%d\n\n%d data source(s) have been polled on %s\n\n"), lpHeader->dwVersion >> 16, lpHeader->dwVersion & 0xffff, dwSources, strBuf);

				if (lpHeader->dwVersion >= 0x00020000)
					//GPU entries are available only in v2.0 and newer shared memory format
				{
					DWORD dwGpus = lpHeader->dwNumGpuEntries;
					//get number of GPUs

					for (DWORD dwGpu = 0; dwGpu < dwGpus; dwGpu++)
						//display info for each GPU
					{
						LPMAHM_SHARED_MEMORY_GPU_ENTRY	lpGpuEntry = (LPMAHM_SHARED_MEMORY_GPU_ENTRY)((LPBYTE)lpHeader + lpHeader->dwHeaderSize + lpHeader->dwNumEntries * lpHeader->dwEntrySize + dwGpu * lpHeader->dwGpuEntrySize);
						//get ptr to the current GPU entry

						CString strStatus = afterburner.DumpGpuEntry(lpGpuEntry);

						for (DWORD dwSource = 0; dwSource < dwSources; dwSource++)
							//display info for each data source
						{
							LPMAHM_SHARED_MEMORY_ENTRY	lpEntry = (LPMAHM_SHARED_MEMORY_ENTRY)((LPBYTE)lpHeader + lpHeader->dwHeaderSize + dwSource * lpHeader->dwEntrySize);
							//get ptr to the current data source entry

							if (afterburner.IsSystemSource(lpEntry->dwSrcId))
								//filter system data sources
								continue;

							if (lpEntry->dwGpu != dwGpu)
								//filter data source entries by GPU index
								continue;

							strStatus += afterburner.DumpEntry(lpEntry);
							//dump data source entry
						}

						if (!strStatus.IsEmpty())
						{
							strBuf.Format(_T("GPU%d\n"), dwGpu + 1);
							//keep in mind that GPU indices are 0-based, so we need to correct index when
							//formatting the GPU section header 

							m_MSIABStatus += strBuf;
							m_MSIABStatus += strStatus;
							m_MSIABStatus += "\n";
						}
					}

					//additional pass for system data sources

					CString strStatus;

					for (DWORD dwSource = 0; dwSource < dwSources; dwSource++)
						//display info for each data source
					{
						LPMAHM_SHARED_MEMORY_ENTRY	lpEntry = (LPMAHM_SHARED_MEMORY_ENTRY)((LPBYTE)lpHeader + lpHeader->dwHeaderSize + dwSource * lpHeader->dwEntrySize);
						//get ptr to the current data source entry

						if (!afterburner.IsSystemSource(lpEntry->dwSrcId))
							//filter specific data sources
							continue;

						strStatus += afterburner.DumpEntry(lpEntry);
						//dump data source entry
					}

					if (!strStatus.IsEmpty())
					{
						m_MSIABStatus += "Global\n";
						m_MSIABStatus += strStatus;
						m_MSIABStatus += "\n";
					}
				}
				else
				{
					for (DWORD dwSource = 0; dwSource < dwSources; dwSource++)
						//display info for each data source
					{
						LPMAHM_SHARED_MEMORY_ENTRY	lpEntry = (LPMAHM_SHARED_MEMORY_ENTRY)((LPBYTE)lpHeader + lpHeader->dwHeaderSize + dwSource * lpHeader->dwEntrySize);
						//get ptr to the current data source entry

						m_MSIABStatus += afterburner.DumpEntry(lpEntry);
						//dump data source entry
					}

					m_MSIABStatus += "\n";
				}
			}
			else
				m_MSIABStatus = "Connected to uninitialized MSI Afterburner Hardware Monitoring shared memory\n\n";

			//m_MSIABStatus += "Hints:\n-Press <Space> to open MSI Afterburner hardware monitoring properties\n-Press <1>...<5> to load and apply the profiles from the corresponding MSI Afterburner profile slots";
		}
		else
		{
			if (afterburner.GetMSIABInstallPath().IsEmpty())
				m_MSIABStatus = "Failed to connect to MSI Afterburner Hardware Monitoring shared memory!\n\nHints:\n-Install MSI Afterburner";
			else
				m_MSIABStatus = "Failed to connect to MSI Afterburner Hardware Monitoring shared memory!\n\nHints:\n-Press <Space> to start MSI Afterburner";
		}

		//Unicode:
		//int len = WideCharToMultiByte(CP_ACP, 0, m_MSIABStatus, -1, NULL, 0, NULL, NULL);
		//char* ptxtTemp = new char[len + 1];
		//WideCharToMultiByte(CP_ACP, 0, m_MSIABStatus, -1, ptxtTemp, len, NULL, NULL);

		//WideChar
		char* t1 = m_MSIABStatus.GetBuffer(m_MSIABStatus.GetLength());
		m_MSIABStatus.ReleaseBuffer();
		std::cout << t1;

		delete[] t1;
	}
}