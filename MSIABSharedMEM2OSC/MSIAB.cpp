#define _AFXDLL

#include "MSIAB.h"
#include "stdafx.h"
#include "MAHMSharedMemory.h"
#include <corecrt_io.h>

LPVOID MSIAB::GetMapAddress()
{
	return m_pMapAddr;
}

/////////////////////////////////////////////////////////////////////////////
// This function is used to connect to MSI Afterburner hardware monitoring
// shared memory
/////////////////////////////////////////////////////////////////////////////
void MSIAB::Connect()
{
	Disconnect();
	//we must disconnect from the previously connected shared memory before
	//connecting to new one

	m_hMapFile = OpenFileMapping(FILE_MAP_ALL_ACCESS, FALSE, _T("MAHMSharedMemory"));

	if (m_hMapFile)
		m_pMapAddr = MapViewOfFile(m_hMapFile, FILE_MAP_ALL_ACCESS, 0, 0, 0);
}
/////////////////////////////////////////////////////////////////////////////
// This function is used to disconnect from MSI Afterburner hardware monitoring
// shared memory
/////////////////////////////////////////////////////////////////////////////
void MSIAB::Disconnect()
{
	if (m_pMapAddr)
		UnmapViewOfFile(m_pMapAddr);

	m_pMapAddr = NULL;

	if (m_hMapFile)
		CloseHandle(m_hMapFile);

	m_hMapFile = NULL;
}

/// <summary>
/// get MSIAfterburner installation path
/// </summary>
/// <returns>null if not found</returns>
CString MSIAB::GetMSIABInstallPath()
{
	//init MSI Afterburner installation path
	if (m_strInstallPath.IsEmpty())
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
					m_strInstallPath = buf;
			}

			RegCloseKey(hKey);
		}
	}

	//validate MSI Afterburner installation path
	if (_taccess(m_strInstallPath, 0))
		m_strInstallPath = "";

	return m_strInstallPath;
}

MSIAB::MSIAB()
{
	m_hMapFile = NULL;
	m_pMapAddr = NULL;

	m_strInstallPath = "";
}

BOOL MSIAB::IsSystemSource(DWORD id)
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

CString MSIAB::DumpGpuEntry(LPMAHM_SHARED_MEMORY_GPU_ENTRY lpGpuEntry)
{
	CString strEntry;
	CString strBuf;

	strBuf.Format(_T("%-32s\t : %-16s\n"), _T("GUID"), lpGpuEntry->szGpuId);
	strEntry += strBuf;

	if (strlen(lpGpuEntry->szDevice))
	{
		CString strDevice;

		if (strlen(lpGpuEntry->szFamily))
			strDevice.Format(_T("%s on %s GPU"), lpGpuEntry->szDevice, lpGpuEntry->szFamily);
		else
			strDevice = lpGpuEntry->szDevice;

		strBuf.Format(_T("%-32s\t : %s\n"), _T("Device"), strDevice);
		strEntry += strBuf;
	}

	if (strlen(lpGpuEntry->szDriver))
	{
		strBuf.Format(_T("%-32s\t : %s\n"), _T("Driver"), lpGpuEntry->szDriver);
		strEntry += strBuf;
	}

	if (strlen(lpGpuEntry->szBIOS))
	{
		strBuf.Format(_T("%-32s\t : %s\n"), _T("BIOS"), lpGpuEntry->szBIOS);
		strEntry += strBuf;
	}

	if (lpGpuEntry->dwMemAmount)
	{
		strBuf.Format(_T("%-32s\t : %d MB\n"), _T("On-board memory"), lpGpuEntry->dwMemAmount / 1024);
		strEntry += strBuf;
	}

	return strEntry;
}

CString MSIAB::DumpEntry(LPMAHM_SHARED_MEMORY_ENTRY lpEntry)
{
	CString strValue;

	if (lpEntry->data == FLT_MAX)
		strValue = "N/A";
	else
	{
		strValue.Format((CString)(lpEntry->szRecommendedFormat), lpEntry->data);
		strValue += " ";
		strValue += lpEntry->szLocalizedSrcUnits;
	}

	CString strEntry;

	strEntry.Format(_T("%-32s\t : %-16s\t"), lpEntry->szLocalizedSrcName, strValue);

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

	return strEntry;
}