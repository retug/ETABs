# -*- coding: utf-8 -*-
"""
Created on Thu Jan 14 13:26:04 2021

@author: aguter
"""
import os
import sys
import comtypes.client

ProgramPath = r"C:\Program Files\Computers and Structures\ETABS 20\ETABS.exe"
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



TableKey = 'Joint Reactions'
FieldKeyList = []

# set the group you want the results for, you can pick either 'All', 'Left Nodes', 'Right Nodes'
GroupName = 'Left Nodes'

TableVersion = 1
FieldsKeysIncluded = []
NumberRecords = 1
TableData = []


JointReactions = SapModel.DatabaseTables.GetTableforDisplayArray(TableKey, FieldKeyList, GroupName, TableVersion, FieldsKeysIncluded, NumberRecords, TableData)


