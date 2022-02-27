#pragma once

#include <string>
#include <list>

using namespace System;

public ref struct ABReportDataGroup
{
public:
	ABReportDataGroup(String^ name, String^ value, String^ unit);
	String^ dataName;
	String^ dataValue;
	String^ dataUnit;
};

/*
public ref struct ABReportDataNode
{
public:
	ABReportDataNode(String^ name, String^ value, String^ unit);
	ABReportDataNode();

	ABReportDataNode^ lastNode;

	ABReportDataGroup data;

	ABReportDataNode^ nextNode;
};

public ref class ABReportData
{
	String^ _dataTime;

	int _length;

	ABReportDataNode^ _header;

public:
	ABReportData();
	String^ GetDataTime();
	void SetDataTime(String^ timeData);

	bool Add(ABReportDataNode^ newNode);
	bool Remove(int index);
	bool Clear();
	ABReportDataGroup^ GetNodeByIndex(int index);

	int GetLength();
};*/
