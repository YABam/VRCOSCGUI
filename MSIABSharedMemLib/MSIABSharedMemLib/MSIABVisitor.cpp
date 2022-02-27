#include <string>
#include <array>
#include <list>
#include "ABReportData.h"
#include "pch.h"
#include "MSIABVisitor.h"

using namespace System;

MSIABVisitor::MSIABVisitor()
{
	m_hMapFile = NULL;
	m_pMapAddr = NULL;

	m_strInstallPath = "";
	resultReport = gcnew System::Collections::Generic::List<ABReportDataGroup^>();
}

/////////////////////////////////////////////////////////////////////////////
// This function is used to disconnect from MSI Afterburner hardware monitoring
// shared memory
/////////////////////////////////////////////////////////////////////////////
void MSIABVisitor::Disconnect()
{
	if (m_pMapAddr)
		UnmapViewOfFile(m_pMapAddr);

	m_pMapAddr = NULL;

	if (m_hMapFile)
		CloseHandle(m_hMapFile);

	m_hMapFile = NULL;
}

/////////////////////////////////////////////////////////////////////////////
// This function is used to connect to MSI Afterburner hardware monitoring
// shared memory
/////////////////////////////////////////////////////////////////////////////
void MSIABVisitor::Connect()
{
	Disconnect();
	//we must disconnect from the previously connected shared memory before
	//connecting to new one

	m_hMapFile = OpenFileMapping(FILE_MAP_ALL_ACCESS, FALSE, _T("MAHMSharedMemory"));

	if (m_hMapFile)
		m_pMapAddr = MapViewOfFile(m_hMapFile, FILE_MAP_ALL_ACCESS, 0, 0, 0);
}

LPVOID MSIABVisitor::GetMapAddress()
{
	return m_pMapAddr;
}

/// <summary>
/// get MSIAfterburner installation path
/// </summary>
/// <returns>null if not found</returns>
System::String^ MSIABVisitor::GetMSIABInstallPath()
{
	//init MSI Afterburner installation path
	if (String::IsNullOrEmpty(m_strInstallPath))
	{
		HKEY hKey;

		if (ERROR_SUCCESS == RegOpenKeyA(HKEY_LOCAL_MACHINE, "Software\\MSI\\Afterburner", &hKey))
		{
			char buf[MAX_PATH];

			DWORD dwSize = MAX_PATH;
			DWORD dwType;

			if (ERROR_SUCCESS == RegQueryValueExA(hKey, "InstallPath", 0, &dwType, (LPBYTE)buf, &dwSize))
			{
				if (dwType == REG_SZ)
					m_strInstallPath = System::Runtime::InteropServices::Marshal::PtrToStringAnsi((IntPtr)buf);
			}

			RegCloseKey(hKey);
		}
	}

	//validate MSI Afterburner installation path (char*)(void*)System::Runtime::InteropServices::Marshal::StringToHGlobalAnsi(m_strInstallPath)
	int iSize = MultiByteToWideChar(CP_ACP, 0, (char*)(void*)System::Runtime::InteropServices::Marshal::StringToHGlobalAnsi(m_strInstallPath), -1, NULL, 0);
	wchar_t* wchar;
	wchar = (wchar_t*)malloc(iSize * sizeof(wchar_t));
	MultiByteToWideChar(CP_ACP, 0, (char*)(void*)System::Runtime::InteropServices::Marshal::StringToHGlobalAnsi(m_strInstallPath), -1, wchar, iSize);

	if (_taccess(wchar, 0))
		m_strInstallPath = "";

	free(wchar);

	return m_strInstallPath;
}

BOOL MSIABVisitor::IsSystemSource(DWORD id)
{
	switch (id)
	{
	case MONITORING_SOURCE_ID_CPU_TEMPERATURE:
	case MONITORING_SOURCE_ID_CPU_USAGE:
	case MONITORING_SOURCE_ID_RAM_USAGE:
	case MONITORING_SOURCE_ID_PAGEFILE_USAGE:
	case MONITORING_SOURCE_ID_CPU_CLOCK:
	case MONITORING_SOURCE_ID_FRAMERATE:
	case MONITORING_SOURCE_ID_FRAMETIME:
		return TRUE;
	}

	return FALSE;
}

void MSIABVisitor::DumpGpuEntry(LPMAHM_SHARED_MEMORY_GPU_ENTRY lpGpuEntry)
{
	//CString strEntry;
	//CString strBuf;

	//resultReport.Add(gcnew ABReportDataNode("GUID", gcnew String(lpGpuEntry->szGpuId), nullptr));
	resultReport->Add(gcnew ABReportDataGroup("GUID", gcnew String(lpGpuEntry->szGpuId), nullptr));

	//strBuf.Format(_T("%-32s\t : %-16s\n"), _T("GUID"), lpGpuEntry->szGpuId);
	//strEntry += strBuf;

	if (strlen(lpGpuEntry->szDevice))
	{
		//CString strDevice;
		String^ strDevice;
		if (strlen(lpGpuEntry->szFamily))
		{
			//strDevice.Format(_T("%s on %s GPU"), lpGpuEntry->szDevice, lpGpuEntry->szFamily);
			strDevice = gcnew String(lpGpuEntry->szDevice) + " on ";
			strDevice += gcnew String(lpGpuEntry->szFamily);
		}
		else
			strDevice = gcnew String(lpGpuEntry->szDevice);

		//strBuf.Format(_T("%-32s\t : %s\n"), _T("Device"), strDevice);
		//strEntry += strBuf;
		//resultReport.Add(gcnew ABReportDataNode(gcnew String("Device"), gcnew String(strDevice), nullptr));
		resultReport->Add(gcnew ABReportDataGroup("Device", gcnew String(strDevice), nullptr));
	}

	if (strlen(lpGpuEntry->szDriver))
	{
		//strBuf.Format(_T("%-32s\t : %s\n"), _T("Driver"), lpGpuEntry->szDriver);
		//strEntry += strBuf;
		//resultReport.Add(gcnew ABReportDataNode(gcnew String("Driver"), gcnew String(lpGpuEntry->szDriver), nullptr));
		resultReport->Add(gcnew ABReportDataGroup("Driver", gcnew String(lpGpuEntry->szDriver), nullptr));
	}

	if (strlen(lpGpuEntry->szBIOS))
	{
		//strBuf.Format(_T("%-32s\t : %s\n"), _T("BIOS"), lpGpuEntry->szBIOS);
		//strEntry += strBuf;
		//resultReport.Add(gcnew ABReportDataNode(gcnew String("BIOS"), gcnew String(lpGpuEntry->szBIOS), nullptr));
		resultReport->Add(gcnew ABReportDataGroup("BIOS", gcnew String(lpGpuEntry->szBIOS), nullptr));
	}

	if (lpGpuEntry->dwMemAmount)
	{
		//strBuf.Format(_T("%-32s\t : %d MB\n"), _T("On-board memory"), lpGpuEntry->dwMemAmount / 1024);
		//strEntry += strBuf;
		//resultReport.Add(gcnew ABReportDataNode(gcnew String("On-board memory"), gcnew String((lpGpuEntry->dwMemAmount / 1024).ToString()), gcnew String("MB")));
		resultReport->Add(gcnew ABReportDataGroup("On-board memory", gcnew String((lpGpuEntry->dwMemAmount / 1024).ToString()), gcnew String("MB")));
	}

	//String^ resultStr = gcnew String(strEntry);
	//strEntry.Empty();

	//return resultStr;
}

void MSIABVisitor::DumpEntry(LPMAHM_SHARED_MEMORY_ENTRY lpEntry)
{
	String^ strValue;
	String^ strUnit;

	if (lpEntry->data == FLT_MAX)
	{
		strValue = "N/A";
		strUnit = "N/A";
	}
	else
	{
		//strValue = gcnew String()
		//strValue = String::Format(gcnew String(lpEntry->szRecommendedFormat), lpEntry->data);
		CString cs;
		cs.Format((CString)(lpEntry->szRecommendedFormat), lpEntry->data);
		strValue = gcnew String(cs);
		cs.Empty();
		//strValue += " ";
		strUnit = gcnew String(lpEntry->szLocalizedSrcUnits);
	}

	//CString strEntry;
	//strEntry.Format(_T("%-32s\t : %-16s\t"), lpEntry->szLocalizedSrcName, strValue);

	//resultReport.Add(gcnew ABReportDataNode(gcnew String(lpEntry->szLocalizedSrcName), gcnew String(strValue), gcnew String(strUnit)));
	resultReport->Add(gcnew ABReportDataGroup(gcnew String(lpEntry->szLocalizedSrcName), gcnew String(strValue), gcnew String(strUnit)));
	/*
	CString strProperties;

	if (lpEntry->dwFlags & MAHM_SHARED_MEMORY_ENTRY_FLAG_SHOW_IN_OSD)
	{
		if (!strProperties.IsEmpty())
			strProperties += ", ";

		strProperties += "OSD";
	}

	if (lpEntry->dwFlags & MAHM_SHARED_MEMORY_ENTRY_FLAG_SHOW_IN_LCD)
	{
		if (!strProperties.IsEmpty())
			strProperties += ", ";

		strProperties += "LCD";
	}

	if (lpEntry->dwFlags & MAHM_SHARED_MEMORY_ENTRY_FLAG_SHOW_IN_TRAY)
	{
		if (!strProperties.IsEmpty())
			strProperties += ", ";

		strProperties += "tray";
	}

	if (!strProperties.IsEmpty())
		strProperties = "in " + strProperties;

	strEntry += strProperties;
	strEntry += "\n";

	String^ resultStr = gcnew String(strEntry);
	strEntry.Empty();
	*/
	//return resultStr;
}

void MSIABVisitor::DumpReportData()
{
	//if we're not connected to MSI Afterburner hardware monitoring shared memory yet - do it now
	if (!GetMapAddress())
		Connect();

	if (GetMapAddress())
		//if we're connected to shared memory, we must check if it is valid or not and reconnect if necessary
	{
		LPMAHM_SHARED_MEMORY_HEADER lpHeader = (LPMAHM_SHARED_MEMORY_HEADER)GetMapAddress();

		if (lpHeader->dwSignature == 0xDEAD)
			//if the memory is marked as dead (e.g. MSI Afterburner was unloaded), we should disconnect from it and
			//try to connect again
			Connect();
	}

	if (GetMapAddress())
	{
		LPMAHM_SHARED_MEMORY_HEADER	lpHeader = (LPMAHM_SHARED_MEMORY_HEADER)GetMapAddress();
		if (lpHeader->dwSignature == 'MAHM')
			//check if we're connected to valid memory
		{
			//CTime time(lpHeader->time);
			dataTime = lpHeader->time;
			resultReport->Clear();
			//resultReport.SetDataTime(gcnew String(time.Format("%d-%m-%Y %H:%M:%S")));

			DWORD dwSources = lpHeader->dwNumEntries;
			//get number of data sources

			//m_MSIABStatus.Format(_T("Connected to MSI Afterburner Hardware Monitoring shared memory v%d.%d\n\n%d data source(s) have been polled on %s\n\n"), lpHeader->dwVersion >> 16, lpHeader->dwVersion & 0xffff, dwSources, strBuf);

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

					DumpGpuEntry(lpGpuEntry);

					for (DWORD dwSource = 0; dwSource < dwSources; dwSource++)
						//display info for each data source
					{
						LPMAHM_SHARED_MEMORY_ENTRY	lpEntry = (LPMAHM_SHARED_MEMORY_ENTRY)((LPBYTE)lpHeader + lpHeader->dwHeaderSize + dwSource * lpHeader->dwEntrySize);
						//get ptr to the current data source entry

						if (IsSystemSource(lpEntry->dwSrcId))
							//filter system data sources
							continue;

						if (lpEntry->dwGpu != dwGpu)
							//filter data source entries by GPU index
							continue;

						DumpEntry(lpEntry);
						//dump data source entry
					}

					//CString temp;
					//temp.Format(_T("%d"), dwGpu + 1);
					resultReport->Add(gcnew ABReportDataGroup("GPU", String::Format("%d", dwGpu), nullptr));
					/*
					if (!strStatus.IsEmpty())
					{
						strBuf.Format(_T("GPU%d\n"), dwGpu + 1);
						//keep in mind that GPU indices are 0-based, so we need to correct index when
						//formatting the GPU section header

						m_MSIABStatus += strBuf;
						m_MSIABStatus += strStatus;
						m_MSIABStatus += "\n";
					}
					*/
				}

				//additional pass for system data sources

				//CString strStatus;

				for (DWORD dwSource = 0; dwSource < dwSources; dwSource++)
					//display info for each data source
				{
					LPMAHM_SHARED_MEMORY_ENTRY	lpEntry = (LPMAHM_SHARED_MEMORY_ENTRY)((LPBYTE)lpHeader + lpHeader->dwHeaderSize + dwSource * lpHeader->dwEntrySize);
					//get ptr to the current data source entry

					if (!IsSystemSource(lpEntry->dwSrcId))
						//filter specific data sources
						continue;

					DumpEntry(lpEntry);
					//dump data source entry
				}

				/*
				if (!strStatus.IsEmpty())
				{
					m_MSIABStatus += "Global\n";
					m_MSIABStatus += strStatus;
					m_MSIABStatus += "\n";
				}
				*/
			}
			else
			{
				for (DWORD dwSource = 0; dwSource < dwSources; dwSource++)
					//display info for each data source
				{
					LPMAHM_SHARED_MEMORY_ENTRY	lpEntry = (LPMAHM_SHARED_MEMORY_ENTRY)((LPBYTE)lpHeader + lpHeader->dwHeaderSize + dwSource * lpHeader->dwEntrySize);
					//get ptr to the current data source entry

					DumpEntry(lpEntry);
					//dump data source entry
				}

				//m_MSIABStatus += "\n";
			}
		}
	}
}

cli::array<ABReportDataGroup^>^ MSIABVisitor::GetReportArray()
{
	_CrtSetBreakAlloc(93);
	DumpReportData();
	cli::array<ABReportDataGroup^>^ resultArray = gcnew cli::array<ABReportDataGroup^>(resultReport->Count);
	for (int i = 0; i < resultReport->Count; i++)
	{
		resultArray[i] = resultReport[i];
	}

	_CrtDumpMemoryLeaks();
	//GC::Collect();

	return resultArray;
}

System::Int64 MSIABVisitor::GetDataTime()
{
	return dataTime;
}
