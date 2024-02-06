import os
import sys
import comtypes.client
import matplotlib.pyplot as plt

#ETABs version 21 and above, Comptypes version 

ProgramPath = r"C:\Program Files\Computers and Structures\ETABS 21\ETABS.exe"
# ModelPath = r"C:\Users\aguter\Desktop\ETABs Models\test shears\shear end rxns.EDB"
helper = comtypes.client.CreateObject('ETABSv1.Helper')
helper = helper.QueryInterface(comtypes.gen.ETABSv1.cHelper)
#create API helper object
ETABSObject = comtypes.client.GetActiveObject("CSI.ETABS.API.ETABSObject")
SapModel = ETABSObject.SapModel
#sets to kip, ft, farienheit
SapModel.SetPresentUnits(4)



NumberTables = 1
TableKey = []
TableName = []
ImportType = []

# this shows all the available tabes that can be accessed
x = SapModel.DatabaseTables.GetAvailableTables(NumberTables,TableKey, TableName, ImportType)

#Returns a list of load cases that are selected for table display. 
NumberSelectedLoadCases = 0
LoadCaseList = []
test = SapModel.DatabaseTables.GetLoadCasesSelectedForDisplay(NumberSelectedLoadCases, LoadCaseList)
###################################################################################################
LoadCaseList = ["TimeHist"]
xtest = SapModel.DatabaseTables.SetLoadCasesSelectedForDisplay(LoadCaseList)
#########################################################################

TableKey = 'Joint Displacements'
FieldKeyList = []
# set the group you want the results for, you can pick either 'All', 'Left Nodes', 'Right Nodes'
GroupName = 'Top Node'

TableVersion = 1
FieldsKeysIncluded = []
NumberRecords = 1
TableData = []

JointDisplacement = SapModel.DatabaseTables.GetTableforDisplayArray(TableKey, FieldKeyList, GroupName, TableVersion, FieldsKeysIncluded, NumberRecords, TableData)

JointXDiplacement = []

#pulls the correct value out for joint dipslacement.
for i, xdisp in enumerate(JointDisplacement[4]):
    if ((i-8)%14) == 0:
        JointXDiplacement.append(xdisp)
Time = []        

for i, xdisp in enumerate(JointDisplacement[4]):
    if ((i-6)%14) == 0:
        Time.append(xdisp)

# Plotting Time versus JointXDisplacement
plt.plot(JointXDiplacement, Time, linestyle='-')
plt.show()  
    

