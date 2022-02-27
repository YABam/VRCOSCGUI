#include "MSIABVisitor.h"
#include <string>
using namespace System;
/*
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

MSIABVisitor::MSIABVisitor()
{
	m_hMapFile = NULL;
	m_pMapAddr = NULL;

	m_strInstallPath = "";
}

LPVOID MSIABVisitor::GetMapAddress()
{
	return m_pMapAddr;
}

/// <summary>
/// get MSIAfterburner installation path
/// </summary>
/// <returns>null if not found</returns>
CString MSIABVisitor::GetMSIABInstallPath()
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
*/

System::String^ Function::say(System::String^ str)
{
	throw gcnew System::NotImplementedException();
	// TODO: 在此处插入 return 语句
}
