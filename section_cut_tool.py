"""
Created on Mon Jan 18 12:34:06 2021
Note this is working with ETABs v19, comtypes v1.1.7. It is not working on my local
machiene only on the remote desktop
@author: aguter
"""
import numpy as np
import os
import sys
import comtypes.client
import matplotlib.pyplot as plt

ProgramPath = r"C:\Program Files\Computers and Structures\ETABS 19\ETABS.exe"
helper = comtypes.client.CreateObject('ETABSv1.Helper')
helper = helper.QueryInterface(comtypes.gen.ETABSv1.cHelper)
#create API helper object
ETABSObject = comtypes.client.GetActiveObject("CSI.ETABS.API.ETABSObject")
SapModel = ETABSObject.SapModel
#sets to kip, ft, farienheit
SapModel.SetPresentUnits(4)
#unlocks model so that sections cuts can be created
SapModel.SetModelIsLocked(False)


areas = SapModel.SelectObj.GetSelected()
area_obj = []
#filters out the floors
for type_obj , beam_num in zip(areas[1],areas[2]) :
    if type_obj == 5 :
        area_obj.append(beam_num)
    else :
        pass

AreaInfo = []
PointData = []
for area in area_obj:
    AreaInfo.append(SapModel.AreaObj.GetPoints(area))
    x = SapModel.AreaObj.GetPoints(area)[1]
    for pnt in x:
        PointData.append(SapModel.PointObj.GetCoordCartesian(pnt)[0:3])

x_areas = np.array(PointData)[:,0]
y_areas = np.array(PointData)[:,1]

print(AreaInfo)
print(PointData)

#sets the Global Coordinates and local coordinate system that points exist within
class Global_Coord :
    def __init__(self, ref_pnt, vector):
        self.ref_pnt = np.array(ref_pnt)
        hyp = (vector[0]**2+vector[1]**2+vector[2]**2)**0.5
        self.vector = np.array(vector)
        self.R = np.array([[vector[0]/hyp, -vector[1]/hyp, 0],[vector[1]/hyp,vector[0]/hyp, 0], [0,0,1]])
        self.R_inv = np.linalg.inv(self.R)
        self.A = np.array(ref_pnt)

#should make this class accept *args, *kwargs
class Point():
    def __init__(self, point, global_cord):
        self.pnt_cord = np.array(point)
        self.R = global_cord.R
        self.R_inv = global_cord.R_inv
        self.A = global_cord.A
    #https://gamedev.stackexchange.com/questions/79765/how-do-i-convert-from-the-global-coordinate-space-to-a-local-space
    def glo_to_loc(self):
        temp = self.pnt_cord-self.A
        loc_coord = np.matmul(self.R_inv,temp)
        return loc_coord
    #https://gamedev.stackexchange.com/questions/79765/how-do-i-convert-from-the-global-coordinate-space-to-a-local-space
    def loc_to_glo(self, loc_coord):
        loc_coord = np.array(loc_coord)
        glo_coord = np.matmul(self.R,loc_coord) + self.A
        return glo_coord

#UI
ref_pnt=[0,0,0]
#UI
vector = [1,0,0]
#set up global coordinate system
global_sys = Global_Coord(ref_pnt, vector)
print(global_sys.R)

local_coords = []
for area_pnts in PointData:
    local_coords.append(Point(area_pnts,global_sys).glo_to_loc())


local_coords_trans = np.transpose(np.array(local_coords))
u_max = max(local_coords_trans[0])
u_min = min(local_coords_trans[0])
v_max = max(local_coords_trans[1])
v_min = min(local_coords_trans[1])

distance = u_max - u_min
# UI Number of slices to make along the diahragm
n_cuts = 100
u_range = np.linspace(u_min+0.01, u_min+distance-0.01, n_cuts)
#UI
height = 10 

#class to make the cutting plane
class four_points():
    def __init__(self, u_range, v_min, v_max, z_coord):
        self.u_range = u_range
        self.v_min = v_min
        self.v_max = v_max
        self.z_coord = z_coord
    def make_4_pnts(self):
        cuts = []
        for i in self.u_range:
            cuts.append([[i,self.v_min, self.z_coord-0.5],[i,self.v_min,self.z_coord+0.5],[i,self.v_max,self.z_coord+0.5],[i,self.v_max,self.z_coord-0.5]])
        return cuts

#Need to make height a variable, last values

sections = four_points(u_range, v_min, v_max, height).make_4_pnts()
global_quad = []
for quad in sections:
    one_quad = []
    for pnt in quad:
        x = Point(pnt,global_sys).loc_to_glo(pnt)
        one_quad.append(x)
    global_quad.append(one_quad)

print(global_quad)
x_plt_pnts = []
y_plt_pnts = []         
for a in global_quad:
    x_plt_pnts.append(a[0][0])
    x_plt_pnts.append(a[2][0])
    y_plt_pnts.append(a[0][1])
    y_plt_pnts.append(a[2][1])


plt.plot(x_plt_pnts,y_plt_pnts)
plt.plot(x_areas,y_areas, 'ro')
plt.show()                

name = []

for i in range(n_cuts):
    temp = "0000"
    len_num = len(str(i))
    len_rem = len(temp)-len_num
    temp_2 = temp[:len_rem] + str(i)
    name.append(temp_2)

print(name)

def make_quad_etabs(name_sect,point):
    name = str(name_sect)
    final = []
    for i in range(4):
        if i == 0:
            test = [name, 'Quads', 'All', 'Analysis', 'Default', '', '', '', '0','0','0','','Top or Right or Positive3','1', '1', '1', str(point[0][0]), str(point[0][1]), str(point[0][2]), '1']
            final.append(test)
        elif i == 1:
            test = [name, '', '','', '', '', '', '', '','','','','','', '1', '2', str(point[1][0]), str(point[1][1]), str(point[1][2]), '']
            final.append(test)
        elif i == 2:
            test = [name, '', '','', '', '', '', '', '','','','','','', '1', '3', str(point[2][0]), str(point[2][1]), str(point[2][2]), '']
            final.append(test)
        elif i == 3:
            test = [name, '', '','', '', '', '', '', '','','','','','', '1', '4', str(point[3][0]), str(point[3][1]), str(point[3][2]), '']
            final.append(test)
    return final

etabs_data_sect = []
for i,(etabs_quad,sec_name) in enumerate(zip(global_quad,name)):
    etabs_data_sect.append(make_quad_etabs(sec_name, etabs_quad))

flat_etabs_data = []
for point in etabs_data_sect:
    temp = []
    for data in point:
        for sing_data in data:
            temp.append(sing_data)
    flat_etabs_data.append(temp)
mega_data = []
for point in flat_etabs_data:
    for ind_pnt in point:
        mega_data.append(ind_pnt)
        
TableKey = 'Section Cut Definitions'
TableVersion = 1
FieldsKeysIncluded = ['Name', 'Defined By', 'Group','Result Type', 'Result Location', 'Location X', 'Location Y', 'Location Z', 'Rotation About Z','Rotation About Y', 'Rotation About X', 'Axis Angle', 'Element Side', 'Number of Quads', 'Quad Number', 'Point Number', 'Quad X', 'Quad Y', 'Quad Z', 'GUID']
NumberRecords = len(flat_etabs_data)
        

y = SapModel.DatabaseTables.SetTableForEditingArray(TableKey,TableVersion,FieldsKeysIncluded, NumberRecords,mega_data)              
    
FillImport = True
z= SapModel.DatabaseTables.ApplyEditedTables(FillImport)
model_has_run = SapModel.Analyze.RunAnalysis()
#sets to kip, ft, farienheit
SapModel.SetPresentUnits(4)

SapModel.Results.Setup.SetCaseSelectedForOutput("EQY+")
NumberResults = 1
SCut = []
LoadCase = []
StepType = []
StepNum = []
F1 = []
F2 = []
F3 = []
M1 = []
M2 = []
M3 = []
test_cut = SapModel.Results.SectionCutAnalysis(NumberResults, SCut, LoadCase, StepType, StepNum, F1, F2, F3, M1, M2, M3)

location = []
for i in sections:
    location.append(i[0][0])
    
shear = test_cut[6]
moment = test_cut[10]


# plt.plot(location,shear, 'ro-')
# plt.xlabel('location')
# plt.ylabel('Shear (kips)')
# plt.title('Shear of the Diaphragm')
# plt.grid(True)
# plt.show()      

# plt.plot(location,moment, 'ro-')
# plt.xlabel('location')
# plt.ylabel('Moment (kip*ft)')
# plt.title('Moment of the Diaphragm')
# plt.grid(True)
# plt.show()
