# -*- coding: utf-8 -*-
"""
Created on Mon Mar  4 17:20:55 2024

@author: aguter
"""
import os
import sys
import comtypes.client

ProgramPath = r"C:\Program Files\Computers and Structures\ETABS 21\ETABS.exe"
helper = comtypes.client.CreateObject('ETABSv1.Helper')
helper = helper.QueryInterface(comtypes.gen.ETABSv1.cHelper)
#create API helper object
ETABSObject = comtypes.client.GetActiveObject("CSI.ETABS.API.ETABSObject")
SapModel = ETABSObject.SapModel

NumberTables = 1
TableKey = []
TableName = []
ImportType = []

# this shows all the available tabes that can be accessed
x = SapModel.DatabaseTables.GetAvailableTables(NumberTables,TableKey, TableName, ImportType)



TableKey = 'Load Pattern Definitions - Auto Wind - ASCE 7-22'
FieldKeyList = []

# set the group you want the results for, you can pick either 'All', 'Left Nodes', 'Right Nodes'
GroupName = 'All'

TableVersion = 0
FieldsKeysIncluded = []
NumberRecords = 1
TableData = []


WindResults = SapModel.DatabaseTables.GetTableforDisplayArray(TableKey, FieldKeyList, GroupName, TableVersion, FieldsKeysIncluded, NumberRecords, TableData)


TableKey2 = 'Load Pattern Definitions - Auto Wind - ASCE 7-22'


ret = SapModel.LoadPatterns.Add("My Sample Wind", 6)
ret2 = SapModel.LoadPatterns.Add("My Sample Wind2", 6)

TableVersion = 0
FieldsKeysIncluded = ['Name', 'IsAuto', 'Exposure','TopStory','BotStory','Parapet','ParapetHt','UserCp','UserCpw',
'UserCplx','UserCpwy','UserCply','ASCECase','e1','e2','WindSpeed','ExpType','GrdElevFact','kzt','GustFact','Kd',
'SolidGross', 'Set', 'WidthType', 'Angle', 'Story', 'Diaphragm', 'Width', 'Depth', 'X', 'Y']
NumberRecords = 2
Data = ["My Sample Wind", "No", "Diaphragms", "Story1", "Base", "No", None, "No",
        None, None, None, None, "Create All", "0.15", "0.15", "100", "C", "1", "1", "0.85", "0.85",
        None, None, None, None, None, None, None, None, None, None]
Data2 = ["My Sample Wind2", "No", "Diaphragms", "Story1", "Base", "No", None, "No",
        None, None, None, None, "Create All", "0.15", "0.15", "100", "C", "1", "1", "0.85", "0.85",
        None, None, None, None, None, None, None, None, None, None]

megadata = Data.copy()
megadata.extend(Data2)


MyNewWind = SapModel.DatabaseTables.SetTableForEditingArray(TableKey2,TableVersion,FieldsKeysIncluded, NumberRecords, megadata)     