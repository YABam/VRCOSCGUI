#include "pch.h"
#include "ABReportData.h"

ABReportDataGroup::ABReportDataGroup(String^ name, String^ value, String^ unit)
{
	dataName = name;
	dataValue = value;
	dataUnit = unit;
}

/*
ABReportData::ABReportData()
{
	_dataTime = gcnew String("");
	_header = nullptr;
	_length = 0;
}

String^ ABReportData::GetDataTime()
{
	return _dataTime;
}

void ABReportData::SetDataTime(String^ timeData)
{
	_dataTime = timeData;
}

bool ABReportData::Add(ABReportDataNode^ newNode)
{
	ABReportDataNode^ created = gcnew ABReportDataNode();
	created->data.dataName = newNode->data.dataName;
	created->data.dataValue = newNode->data.dataValue;
	created->data.dataUnit = newNode->data.dataUnit;

	//Insert this node to list
	if (_header == nullptr)
	{
		//this list is empty
		created->lastNode = nullptr;
		created->nextNode = nullptr;
		_header = created;
		_length = 1;
	}
	else
	{
		ABReportDataNode^ tail = _header;
		for (int i = 0; i < _length - 1; i++)
		{
			tail = tail->nextNode;
		}
		//got tail node
		created->lastNode = tail;
		created->nextNode = nullptr;
		tail->nextNode = created;
		_length++;
	}
	return true;
}

bool ABReportData::Remove(int index)
{
	if (index >= _length)
	{
		return false;
	}
	else
	{
		if (index == 0)//remove head
		{
			ABReportDataNode^ newHeader = _header->nextNode;
			_header = newHeader;
		}
		else
		{
			ABReportDataNode^ target = _header;
			for (int i = 0; i < index; i++)
			{
				target = target->nextNode;
			}
			//got target node
			target->lastNode->nextNode = target->nextNode;
			target->nextNode->lastNode = target->lastNode;
		}
		GC::Collect();
		_length--;
	}
	return true;
}

bool ABReportData::Clear()
{
	for (int i = 0; i < _length; i++)
	{
		Remove(_length - 1);
	}
	_length = 0;
	_header = nullptr;
	_dataTime = gcnew String("");

	return true;
}

ABReportDataGroup^ ABReportData::GetNodeByIndex(int index)
{
	ABReportDataGroup^ tempNode = gcnew ABReportDataGroup();
	ABReportDataNode^ target = _header;
	for (int i = 0; i < index; i++)
	{
		target = target->nextNode;
	}
	tempNode->dataName = target->data.dataName;
	tempNode->dataUnit = target->data.dataUnit;
	tempNode->dataValue = target->data.dataValue;

	return tempNode;
}

int ABReportData::GetLength()
{
	return _length;
}

ABReportDataNode::ABReportDataNode()
{
	lastNode = nullptr;
	nextNode = nullptr;

	data.dataName = gcnew String("");
	data.dataValue = gcnew String("");
	data.dataUnit = gcnew String("");
}

ABReportDataNode::ABReportDataNode(String^ name, String^ value, String^ unit)
{
	data.dataName = name;
	data.dataValue = value;
	data.dataUnit = unit;

	lastNode = nullptr;
	nextNode = nullptr;
}*/
