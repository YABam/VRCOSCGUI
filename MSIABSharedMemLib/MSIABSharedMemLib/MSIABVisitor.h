#pragma once

#include "pch.h"
#include "MAHMSharedMemory.h"
#include "ABReportData.h"
#include <string>
#include <list>

using namespace std;

public ref class MSIABVisitor
{
protected:
	System::String^ m_strInstallPath;
	LPVOID m_pMapAddr;//address of shared memory
	HANDLE m_hMapFile;
	
	System::Int64 dataTime;

	//ABReportData resultReport;
	System::Collections::Generic::List<ABReportDataGroup^>^ resultReport;

	void Connect();
	void Disconnect();

	void DumpReportData();

	LPVOID GetMapAddress();

	System::String^ GetMSIABInstallPath();
	BOOL IsSystemSource(DWORD id);

	void DumpGpuEntry(LPMAHM_SHARED_MEMORY_GPU_ENTRY lpGpuEntry);
	void DumpEntry(LPMAHM_SHARED_MEMORY_ENTRY lpEntry);

public:
	MSIABVisitor();

	cli::array<ABReportDataGroup^>^ GetReportArray();
	System::Int64 GetDataTime();
	//System::String^
};