Imports System
Imports System.Data
Imports BusinesLayer
Imports DatabaseLayer
Public Class clsBranchMaster
    Private objDBL As New DatabaseLayer.DBHelper
    Private objclsFASGeneral As New clsFASGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Public iID As Integer
    Public sCode As String
    Public sDesc As String
    Public sCategory As String
    Public sRemarks As String
    Public iKeyComponent As Integer
    Public sModule As String
    Public iRiskCategory As Integer
    Public sStatus As String
    Public sdelflag As String
    Public iCrBy As Integer
    Public iUpdatedBy As Integer
    Public sIpAddress As String
    Public iCompId As Integer
    Public iYearID As Integer
    Public dStartValue As String
    Public dEndValue As String
    Public sName As String
    Public sColor As String
    Public sFLAG As String

    ' Area,Methodology,SampleSize Master
    Public Function LoadBCMMaster(ByVal sAC As String, ByVal iACID As Integer, ByVal sCategory As String) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select cmm_ID,SubString(cmm_Desc,0,200) As cmm_Desc from Content_Management_Master Where cmm_Category='" & sCategory & "' and CMM_CompID=" & iACID & " Order by cmm_Desc"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    'BCM Rating
    Public Function LoadBCMRating(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select CMAR_ID,SubString(CMAR_Name,0,200) As CMAR_Name from CMARating Where CMAR_CompId=" & iACID & " And  CMAR_YearID=" & iYearID & "  order by CMAR_Name"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    'BCM Core Process 
    Public Function LoadCoreProcess(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select CMACR_ID,SubString(CMACR_Name,0,200) As CMACR_Name from CMARating_CoreProcess Where CMACR_CompId=" & iACID & " And CMACR_YearID=" & iYearID & " order by CMACR_Name"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    'BCM Support Process Description
    Public Function LoadSupportProcess(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select CMASR_ID,SubString(CMASR_Name,0,200) As CMASR_Name from CMARating_SupportProcess Where CMASR_CompId=" & iACID & " And CMASR_YearID=" & iYearID & " order by CMASR_Name "
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    ' Color
    Public Function LoadColors(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select * from Trace_Color_Master Where TC_CompID=" & iACID & " "
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    ' Area,Methodology,SampleSize Description
    Public Function LoadDescription(ByVal sAC As String, ByVal iACID As Integer, ByVal icmmID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select cmm_Desc,cmm_Code,cms_Remarks,CMM_Delflag from Content_Management_Master Where cmm_ID=" & icmmID & " and CMM_CompID=" & iACID & " order by cmm_Desc"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    'BCM Core Process Description
    Public Function LoadCoreProcessDescription(ByVal sAC As String, ByVal iACID As Integer, ByVal icmmID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select * from CMARating_CoreProcess Where CMACR_ID=" & icmmID & " And CMACR_CompId=" & iACID & " And CMACR_YearID=" & iYearID & " order by CMACR_Name"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    'BCM Support Process Description
    Public Function LoadSupportProcessDescription(ByVal sAC As String, ByVal iACID As Integer, ByVal icmmID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select * from CMARating_SupportProcess Where CMASR_ID=" & icmmID & " And CMASR_CompId=" & iACID & " And CMASR_YearID=" & iYearID & " order by CMASR_Name"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    'BCM Rating Description
    Public Function LoadBCMRatingDescription(ByVal sAC As String, ByVal iACID As Integer, ByVal icmmID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select * from CMARating Where CMAR_ID=" & icmmID & " And CMAR_CompId=" & iACID & " And CMAR_YearID=" & iYearID & " order by CMAR_Name"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    ' Area,Methodology,SampleSize Master Details 
    Public Function SaveMasterDetails(ByVal sAC As String, ByVal objclsMaster As clsBranchMaster) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(15) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@cmm_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsMaster.iID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@cmm_Code", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objclsMaster.sCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@cmm_Desc", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objclsMaster.sDesc
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@cmm_Category", OleDb.OleDbType.VarChar, 3)
            ObjParam(iParamCount).Value = objclsMaster.sCategory
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@cms_Remarks", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objclsMaster.sRemarks
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@cms_KeyComponent", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsMaster.iKeyComponent
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@cms_Module", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objclsMaster.sModule
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CMM_RiskCategory", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsMaster.iRiskCategory
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CMM_Status", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objclsMaster.sStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@cmm_delflag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objclsMaster.sdelflag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CMM_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsMaster.iCrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CMM_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsMaster.iUpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CMM_IpAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsMaster.sIpAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CMM_CompId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsMaster.iCompId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spContent_Management_Master", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    'BCM Rating Details
    Public Function SaveBCMRatingDetails(ByVal sAC As String, ByVal objclsMaster As clsBranchMaster) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(14) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CMAR_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsMaster.iID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CMAR_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsMaster.iYearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CMAR_StartValue", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objclsMaster.dStartValue
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CMAR_EndValue", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objclsMaster.dEndValue
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CMAR_Desc", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objclsMaster.sDesc
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CMAR_Name", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objclsMaster.sName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CMAR_Color", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objclsMaster.sColor
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CMAR_Flag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objclsMaster.sFLAG
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CMAR_STATUS", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objclsMaster.sStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CMAR_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsMaster.iCrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CMAR_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsMaster.iUpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CMAR_CompId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsMaster.iCompId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CMAR_IpAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsMaster.sIpAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spCMARating", 1, Arr, ObjParam)
            Return Arr

        Catch ex As Exception
            Throw
        End Try
    End Function
    'BCM Core Process Details
    Public Function SavecCoreProcessDetails(ByVal sAC As String, ByVal objclsMaster As clsBranchMaster) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(14) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CMACR_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsMaster.iID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CMACR_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsMaster.iYearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CMACR_StartValue", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objclsMaster.dStartValue
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CMACR_EndValue", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objclsMaster.dEndValue
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CMACR_Desc", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objclsMaster.sDesc
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CMACR_Name", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objclsMaster.sName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CMACR_Color", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objclsMaster.sColor
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CMACR_Flag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objclsMaster.sFLAG
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CMACR_STATUS", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objclsMaster.sStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CMACR_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsMaster.iCrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CMACR_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsMaster.iUpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CMACR_IpAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsMaster.sIpAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CMACR_CompId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsMaster.iCompId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spCMARating_CoreProcess", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    'BCM Support Process Details
    Public Function SaveSupportProcessDetails(ByVal sAC As String, ByVal objclsMaster As clsBranchMaster) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(14) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CMASR_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsMaster.iID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CMASR_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsMaster.iYearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CMASR_StartValue", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objclsMaster.dStartValue
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CMASR_EndValue", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objclsMaster.dEndValue
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CMASR_Desc", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objclsMaster.sDesc
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CMASR_Name", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objclsMaster.sName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CMASR_Color", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objclsMaster.sColor
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CMASR_Flag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objclsMaster.sFLAG
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CMASR_STATUS", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objclsMaster.sStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CMASR_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsMaster.iCrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CMASR_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsMaster.iUpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CMASR_CompId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsMaster.iCompId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CMASR_IpAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsMaster.sIpAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spCMARating_SupportProcess", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadOverAllRating(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iStatus As Integer, ByVal sSearch As String) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim dtTab As New DataTable
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("ID")
            dtTab.Columns.Add("Name")
            dtTab.Columns.Add("Desc")
            dtTab.Columns.Add("Color")
            dtTab.Columns.Add("Start")
            dtTab.Columns.Add("End")
            dtTab.Columns.Add("Status")

            sSql = "Select * from CMARating Where CMAR_CompID= " & iACID & " And CMAR_YearID=" & iYearID & ""
            If iStatus = 0 Then
                sSql = sSql & " And CMAR_FLAG ='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & " And CMAR_FLAG='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " And CMAR_FLAG='W'" 'Waiting for approval
            End If
            If sSearch <> "" Then
                sSql = sSql & " And (CMAR_Name Like '" & sSearch & "%')"
            End If
            sSql = sSql & " order by CMAR_StartValue"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)

            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                dr("SrNo") = i + 1
                dr("ID") = dt.Rows(i)("CMAR_ID")
                dr("Name") = dt.Rows(i)("CMAR_Name")
                dr("Desc") = dt.Rows(i)("CMAR_Desc")
                dr("Color") = dt.Rows(i)("CMAR_Color")
                dr("Start") = dt.Rows(i)("CMAR_StartValue")
                dr("End") = dt.Rows(i)("CMAR_EndValue")
                If dt.Rows(i)("CMAR_FLAG") = "A" Then
                    dr("Status") = "Activated"
                ElseIf dt.Rows(i)("CMAR_FLAG") = "D" Then
                    dr("Status") = "De-Activated"
                ElseIf dt.Rows(i)("CMAR_FLAG") = "W" Then
                    dr("Status") = "Waiting for Approval"
                End If
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadGridCoreProcessHeatMap(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iStatus As Integer, ByVal sSearch As String) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim dtTab As New DataTable
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("ID")
            dtTab.Columns.Add("Name")
            dtTab.Columns.Add("Desc")
            dtTab.Columns.Add("Color")
            dtTab.Columns.Add("Start")
            dtTab.Columns.Add("End")
            dtTab.Columns.Add("Status")
            sSql = "Select * from CMARating_CoreProcess Where CMACR_CompID= " & iACID & " And CMACR_YearID=" & iYearID & ""
            If iStatus = 0 Then
                sSql = sSql & " And CMACR_FLAG ='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & " And CMACR_FLAG='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " And CMACR_FLAG='W'" 'Waiting for approval
            End If
            If sSearch <> "" Then
                sSql = sSql & " And (CMACR_Name Like '" & sSearch & "%')"
            End If
            sSql = sSql & " order by CMACR_StartValue"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)

            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                dr("SrNo") = i + 1
                dr("ID") = dt.Rows(i)("CMACR_ID")
                dr("Name") = dt.Rows(i)("CMACR_Name")
                dr("Desc") = dt.Rows(i)("CMACR_Desc")
                dr("Color") = dt.Rows(i)("CMACR_Color")
                dr("Start") = dt.Rows(i)("CMACR_StartValue")
                dr("End") = dt.Rows(i)("CMACR_EndValue")
                If dt.Rows(i)("CMACR_FLAG") = "A" Then
                    dr("Status") = "Activated"
                ElseIf dt.Rows(i)("CMACR_FLAG") = "D" Then
                    dr("Status") = "De-Activated"
                ElseIf dt.Rows(i)("CMACR_FLAG") = "W" Then
                    dr("Status") = "Waiting for Approval"
                End If
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadGridSupportProcessHeatMap(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iStatus As Integer, ByVal sSearch As String) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim dtTab As New DataTable
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("ID")
            dtTab.Columns.Add("Name")
            dtTab.Columns.Add("Desc")
            dtTab.Columns.Add("Color")
            dtTab.Columns.Add("Start")
            dtTab.Columns.Add("End")
            dtTab.Columns.Add("Status")

            sSql = "Select * from CMARating_SupportProcess Where CMASR_CompID= " & iACID & " And CMASR_YearID=" & iYearID & ""
            If iStatus = 0 Then
                sSql = sSql & " And CMASR_FLAG ='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & " And CMASR_FLAG='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " And CMASR_FLAG='W'" 'Waiting for approval
            End If
            If sSearch <> "" Then
                sSql = sSql & " And (CMASR_Name Like '" & sSearch & "%')"
            End If
            sSql = sSql & " order by CMASR_StartValue"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)

            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                dr("SrNo") = i + 1
                dr("ID") = dt.Rows(i)("CMASR_ID")
                dr("Name") = dt.Rows(i)("CMASR_Name")
                dr("Desc") = dt.Rows(i)("CMASR_Desc")
                dr("Color") = dt.Rows(i)("CMASR_Color")
                dr("Start") = dt.Rows(i)("CMASR_StartValue")
                dr("End") = dt.Rows(i)("CMASR_EndValue")
                If dt.Rows(i)("CMASR_FLAG") = "A" Then
                    dr("Status") = "Activated"
                ElseIf dt.Rows(i)("CMASR_FLAG") = "D" Then
                    dr("Status") = "De-Activated"
                ElseIf dt.Rows(i)("CMASR_FLAG") = "W" Then
                    dr("Status") = "Waiting for Approval"
                End If
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadBCMMasterGrid(ByVal sAC As String, ByVal iACID As Integer, ByVal sCategory As String, ByVal iStatus As Integer, ByVal sSearch As String) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dttab As New DataTable
        Dim dr As DataRow
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("ID")
            dt.Columns.Add("Name")
            dt.Columns.Add("Status")

            sSql = "Select  cmm_ID,cmm_desc,cmm_delflag from Content_Management_Master Where cmm_Category='" & sCategory & "' and CMM_CompID=" & iACID & ""
            If iStatus = 0 Then
                sSql = sSql & " And cmm_delflag ='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & " And cmm_delflag='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " And cmm_delflag='W'" 'Waiting for approval
            End If
            If sSearch <> "" Then
                sSql = sSql & " And (cmm_desc Like '" & sSearch & "%')"
            End If
            sSql = sSql & " Order by cmm_Desc"
            dttab = objDBL.SQLExecuteDataTable(sAC, sSql)

            If dttab.Rows.Count > 0 Then
                For i = 0 To dttab.Rows.Count - 1
                    dr = dt.NewRow
                    dr("SrNo") = i + 1
                    dr("ID") = dttab.Rows(i)("cmm_ID")
                    dr("Name") = dttab.Rows(i)("cmm_desc")
                    If dttab.Rows(i)("cmm_delflag") = "A" Then
                        dr("Status") = "Activated"
                    ElseIf dttab.Rows(i)("cmm_delflag") = "D" Then
                        dr("Status") = "De-Activated"
                    ElseIf dttab.Rows(i)("cmm_delflag") = "W" Then
                        dr("Status") = "Waiting for Approval"
                    End If
                    dt.Rows.Add(dr)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub BranchMasterApproveStatus(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iID As Integer, ByVal sIPAddress As String, ByVal sType As String)
        Dim sSql As String
        Try
            sSql = "Update Content_Management_Master set"
            If sType = "Created" Then
                sSql = sSql & " cmm_DelFlag='A',CMM_Status='A',CMM_ApprovedBy=" & iUserID & ", CMM_ApprovedOn=Getdate(),"
            ElseIf sType = "DeActivated" Then
                sSql = sSql & " cmm_DelFlag='D',CMM_Status='AD',CMM_DeletedBy=" & iUserID & ", CMM_DeletedOn=Getdate(),"
            ElseIf sType = "Activated" Then
                sSql = sSql & " cmm_DelFlag='A',CMM_Status='AR',CMM_RecallBy=" & iUserID & ", CMM_RecallOn=Getdate(),"
            End If
            sSql = sSql & " CMM_IPAddress='" & sIPAddress & "' Where CMM_CompID=" & iACID & " And cmm_ID=" & iID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BranchProcessApproveStatus(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iID As Integer, ByVal sIPAddress As String, ByVal sType As String, ByVal sTableType As String)
        Dim sSql As String
        Try
            If sTableType = "R" Then
                sSql = "Update CMARating set"
                If sType = "Created" Then
                    sSql = sSql & " CMAR_FLAG='A',CMAR_STATUS='A',CMAR_ApprovedBy=" & iUserID & ", CMAR_ApprovedOn=Getdate(),"
                ElseIf sType = "DeActivated" Then
                    sSql = sSql & " CMAR_FLAG='D',CMAR_STATUS='AD',CMAR_DeletedBy=" & iUserID & ", CMAR_DeletedOn=Getdate(),"
                ElseIf sType = "Activated" Then
                    sSql = sSql & " CMAR_FLAG='A',CMAR_STATUS='AR',CMAR_RecallBy=" & iUserID & ", CMAR_RecallOn=Getdate(),"
                End If
                sSql = sSql & "CMAR_IPAddress='" & sIPAddress & "' Where CMAR_CompId=" & iACID & " And CMAR_ID=" & iID & ""
                objDBL.SQLExecuteNonQuery(sAC, sSql)
            ElseIf sTableType = "CR" Then
                sSql = "Update CMARating_CoreProcess set"
                If sType = "Created" Then
                    sSql = sSql & " CMACR_FLAG='A',CMACR_STATUS='A',CMACR_ApprovedBy=" & iUserID & ", CMACR_ApprovedOn=Getdate(),"
                ElseIf sType = "DeActivated" Then
                    sSql = sSql & " CMACR_FLAG='D',CMACR_STATUS='AD',CMACR_DeletedBy=" & iUserID & ", CMACR_DeletedOn=Getdate(),"
                ElseIf sType = "Activated" Then
                    sSql = sSql & " CMACR_FLAG='A',CMACR_STATUS='AR',CMACR_RecallBy=" & iUserID & ", CMACR_RecallOn=Getdate(),"
                End If
                sSql = sSql & "CMACR_IPAddress='" & sIPAddress & "' Where CMACR_CompId=" & iACID & " And CMACR_ID=" & iID & ""
                objDBL.SQLExecuteNonQuery(sAC, sSql)
            ElseIf sTableType = "SR" Then
                sSql = "Update CMARating_SupportProcess set"
                If sType = "Created" Then
                    sSql = sSql & " CMASR_FLAG='A',CMASR_STATUS='A',CMASR_ApprovedBy=" & iUserID & ", CMASR_ApprovedOn=Getdate(),"
                ElseIf sType = "DeActivated" Then
                    sSql = sSql & " CMASR_FLAG='D',CMASR_STATUS='AD',CMASR_DeletedBy=" & iUserID & ", CMASR_DeletedOn=Getdate(),"
                ElseIf sType = "Activated" Then
                    sSql = sSql & " CMASR_FLAG='A',CMASR_STATUS='AR',CMASR_RecallBy=" & iUserID & ", CMASR_RecallOn=Getdate(),"
                End If
                sSql = sSql & " CMASR_IPAddress='" & sIPAddress & "' Where CMASR_CompId=" & iACID & " And CMASR_ID=" & iID & ""
                objDBL.SQLExecuteNonQuery(sAC, sSql)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function CheckExistingDetailsBCMMasters(ByVal sAC As String, ByVal iACID As Integer, ByVal sDesc As Object, ByVal sMasterType As String, ByVal iDescID As Integer, ByVal iYearID As Integer) As Boolean
        Dim sSql As String = ""
        Try
            If sMasterType = "AR" Or sMasterType = "M" Or sMasterType = "SS" Then
                sSql = "Select CMM_ID from Content_Management_Master where CMM_CompID=" & iACID & " And  CMM_Desc = '" & sDesc & "' And Cmm_Category='" & sMasterType & "'"
                If iDescID > 0 Then
                    sSql = sSql & " And CMM_ID<>" & iDescID & ""
                End If
            ElseIf sMasterType = "R" Then
                sSql = "Select CMAR_ID from CMARating where CMAR_CompID=" & iACID & " And CMAR_Name = '" & sDesc & "' and CMAR_YearID=" & iYearID & ""
                If iDescID > 0 Then
                    sSql = sSql & " And CMAR_ID<>" & iDescID & ""
                End If
            ElseIf sMasterType = "CR" Then
                sSql = "Select CMACR_ID from CMARating_CoreProcess where CMACR_CompID=" & iACID & " And CMACR_Name = '" & sDesc & "' and CMACR_YearID=" & iYearID & ""
                If iDescID > 0 Then
                    sSql = sSql & " And CMACR_ID<>" & iDescID & ""
                End If
            ElseIf sMasterType = "SR" Then
                sSql = "Select CMASR_ID from CMARating_SupportProcess where CMASR_CompID=" & iACID & " And CMASR_Name = '" & sDesc & "' and CMASR_YearID=" & iYearID & ""
                If iDescID > 0 Then
                    sSql = sSql & " And CMASR_ID<>" & iDescID & ""
                End If
            End If
            CheckExistingDetailsBCMMasters = objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadSysBCMSupportProcessReports(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String
        Dim dtTabl As New DataTable, dt As New DataTable
        Dim dr As DataRow
        Dim Flag As String = ""
        Try
            dt.Columns.Add("Master")
            dt.Columns.Add("Name")
            dt.Columns.Add("Risk audit rating")
            dt.Columns.Add("Start value")
            dt.Columns.Add("End value")
            dt.Columns.Add("Color")
            dt.Columns.Add("Created by")
            dt.Columns.Add("Created on")
            dt.Columns.Add("Approved by")
            dt.Columns.Add("Approved on")

            sSql = "Select a.CMASR_Name,a.CMASR_Desc,a.CMASR_StartValue,a.CMASR_EndValue,a.CMASR_Color,convert(char(10),a.CMASR_CrOn,103) CMASR_CrOn,convert(char(10),a.CMASR_ApprovedOn,103) CMASR_ApprovedOn,"
            sSql = sSql & " c.usr_FullName As CMASR_ApprovedBy,b.usr_FullName As CMASR_CrBy from CMARating_SupportProcess a Left join Sad_UserDetails b  On a.CMASR_CrBy=b.usr_Id "
            sSql = sSql & " Left join Sad_UserDetails c  on a.CMASR_ApprovedBy=c.usr_Id  where CMASR_CompID= " & iACID & " And CMASR_YearID=" & iYearID & " order by CMASR_Name"
            dtTabl = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtTabl.Rows.Count - 1
                dr = dt.NewRow
                If Flag.Contains("BCM support process rating") Then
                    dr("Master") = ""
                Else
                    dr("Master") = "BCM support process rating"
                    Flag = dr("Master")
                End If
                dr("Name") = dtTabl.Rows(i)("CMASR_Name")
                dr("Risk audit rating") = dtTabl.Rows(i)("CMASR_Desc")
                dr("Color") = dtTabl.Rows(i)("CMASR_Color")
                dr("Start value") = dtTabl.Rows(i)("CMASR_StartValue")
                dr("End value") = dtTabl.Rows(i)("CMASR_EndValue")
                If IsDBNull(dtTabl.Rows(i)("CMASR_CrBy")) = False Then
                    dr("Created by") = dtTabl.Rows(i)("CMASR_CrBy")
                End If
                If IsDBNull(dtTabl.Rows(i)("CMASR_CrOn")) = False Then
                    dr("Created On") = dtTabl.Rows(i)("CMASR_CrOn")
                End If
                If IsDBNull(dtTabl.Rows(i)("CMASR_ApprovedBy")) = False Then
                    dr("Approved by") = dtTabl.Rows(i)("CMASR_ApprovedBy")
                End If
                If IsDBNull(dtTabl.Rows(i)("CMASR_ApprovedOn")) = False Then
                    dr("Approved On") = dtTabl.Rows(i)("CMASR_ApprovedOn")
                End If
                dt.Rows.Add(dr)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSysBCMCoreProcessReports(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String
        Dim dtTabl As New DataTable, dt As New DataTable
        Dim dr As DataRow
        Dim Flag As String = ""
        Try
            dt.Columns.Add("Master")
            dt.Columns.Add("Name")
            dt.Columns.Add("Risk audit rating")
            dt.Columns.Add("Start value")
            dt.Columns.Add("End value")
            dt.Columns.Add("Color")
            dt.Columns.Add("Created by")
            dt.Columns.Add("Created On")
            dt.Columns.Add("Approved by")
            dt.Columns.Add("Approved On")
            sSql = "Select a.CMACR_Name,a.CMACR_Desc,a.CMACR_StartValue,a.CMACR_EndValue,a.CMACR_Color,convert(Char(10),a.CMACR_CrOn,103) CMACR_CrOn,convert(Char(10),a.CMACR_ApprovedOn,103) CMACR_ApprovedOn,"
            sSql = sSql & " c.usr_FullName As CMACR_ApprovedBy,b.usr_FullName As CMACR_CrBy from CMARating_CoreProcess a Left join Sad_UserDetails b  On a.CMACR_CrBy=b.usr_Id "
            sSql = sSql & " Left join Sad_UserDetails c on a.CMACR_ApprovedBy=c.usr_Id  where CMACR_CompID= " & iACID & " And CMACR_YearID=" & iYearID & " order by CMACR_Name"
            dtTabl = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtTabl.Rows.Count - 1
                dr = dt.NewRow
                If Flag.Contains("BCM core process rating") Then
                    dr("Master") = ""
                Else
                    dr("Master") = "BCM core process rating"
                    Flag = dr("Master")
                End If
                dr("Name") = dtTabl.Rows(i)("CMACR_Name")
                dr("Risk audit rating") = dtTabl.Rows(i)("CMACR_Desc")
                dr("Color") = dtTabl.Rows(i)("CMACR_Color")
                dr("Start value") = dtTabl.Rows(i)("CMACR_StartValue")
                dr("End value") = dtTabl.Rows(i)("CMACR_EndValue")
                If IsDBNull(dtTabl.Rows(i)("CMACR_CrBy")) = False Then
                    dr("Created by") = dtTabl.Rows(i)("CMACR_CrBy")
                End If
                If IsDBNull(dtTabl.Rows(i)("CMACR_CrOn")) = False Then
                    dr("Created on") = dtTabl.Rows(i)("CMACR_CrOn")
                End If
                If IsDBNull(dtTabl.Rows(i)("CMACR_ApprovedBy")) = False Then
                    dr("Approved by") = dtTabl.Rows(i)("CMACR_ApprovedBy")
                End If
                If IsDBNull(dtTabl.Rows(i)("CMACR_ApprovedOn")) = False Then
                    dr("Approved on") = dtTabl.Rows(i)("CMACR_ApprovedOn")
                End If
                dt.Rows.Add(dr)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSysBCMRatingReports(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String
        Dim dtTabl As New DataTable, dt As New DataTable
        Dim dr As DataRow
        Dim Flag As String = ""
        Try
            dt.Columns.Add("Master")
            dt.Columns.Add("Name")
            dt.Columns.Add("Risk audit rating")
            dt.Columns.Add("Start value")
            dt.Columns.Add("End value")
            dt.Columns.Add("Color")
            dt.Columns.Add("Created by")
            dt.Columns.Add("Created on")
            dt.Columns.Add("Approved by")
            dt.Columns.Add("Approved on")
            sSql = "Select a.CMAR_Name,a.CMAR_Desc,a.CMAR_StartValue,a.CMAR_EndValue,a.CMAR_Color,convert(char(10),a.CMAR_CrOn,103) CMAR_CrOn,convert(char(10),a.CMAR_ApprovedOn,103) CMAR_ApprovedOn,"
            sSql = sSql & " c.usr_FullName As CMAR_ApprovedBy,b.usr_FullName As CMAR_CrBy from CMARating a Left join Sad_UserDetails b  On a.CMAR_CrBy=b.usr_Id "
            sSql = sSql & " Left join Sad_UserDetails c  on a.CMAR_ApprovedBy=c.usr_Id where CMAR_CompID= " & iACID & " And CMAR_YearID=" & iYearID & " order by CMAR_Name"
            dtTabl = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtTabl.Rows.Count - 1
                dr = dt.NewRow
                If Flag.Contains("BCM rating") Then
                    dr("Master") = ""
                Else
                    dr("Master") = "BCM rating"
                    Flag = dr("Master")
                End If
                dr("Name") = dtTabl.Rows(i)("CMAR_Name")
                dr("Risk audit rating") = dtTabl.Rows(i)("CMAR_Desc")
                dr("Color") = dtTabl.Rows(i)("CMAR_Color")
                dr("Start value") = dtTabl.Rows(i)("CMAR_StartValue")
                dr("End value") = dtTabl.Rows(i)("CMAR_EndValue")
                If IsDBNull(dtTabl.Rows(i)("CMAR_CrBy")) = False Then
                    dr("Created by") = dtTabl.Rows(i)("CMAR_CrBy")
                End If
                If IsDBNull(dtTabl.Rows(i)("CMAR_CrOn")) = False Then
                    dr("Created on") = dtTabl.Rows(i)("CMAR_CrOn")
                End If
                If IsDBNull(dtTabl.Rows(i)("CMAR_ApprovedBy")) = False Then
                    dr("Approved by") = dtTabl.Rows(i)("CMAR_ApprovedBy")
                End If
                If IsDBNull(dtTabl.Rows(i)("CMAR_ApprovedOn")) = False Then
                    dr("Approved on") = dtTabl.Rows(i)("CMAR_ApprovedOn")
                End If
                dt.Rows.Add(dr)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSysBCMAreaReports(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Dim dtTabl As New DataTable, dt As New DataTable
        Dim dr As DataRow
        Dim Flag As String = ""
        Try
            dt.Columns.Add("Master")
            dt.Columns.Add("Name")
            dt.Columns.Add("Code")
            dt.Columns.Add("Remarks")
            dt.Columns.Add("Created by")
            dt.Columns.Add("Created on")
            dt.Columns.Add("Approved by")
            dt.Columns.Add("Approved on")
            sSql = "Select a.cmm_Category,a.cmm_Desc,a.cmm_Code,a.cms_Remarks,convert(char(10),a.CMM_CrOn,103) CMM_CrOn,convert(char(10),a.CMM_ApprovedOn,103) CMM_ApprovedOn,"
            sSql = sSql & " b.usr_FullName As CMM_CrBy,c.usr_FullName As CMM_ApprovedBy from Content_Management_Master a Left join Sad_UserDetails b  On a.CMM_CrBy=b.usr_Id "
            sSql = sSql & " Left join Sad_UserDetails c  on a.CMM_ApprovedBy=c.usr_Id Where CMM_CompID= " & iACID & " order by cmm_Desc"
            dtTabl = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtTabl.Rows.Count - 1
                dr = dt.NewRow
                If IsDBNull(dtTabl.Rows(i)("cmm_Category")) = False Then
                    If (dtTabl.Rows(i)("cmm_Category") = "AR") Then
                        If (dtTabl.Rows(i)("cmm_Category") = Flag) Then
                            dr("Master") = ""
                        Else
                            dr("Master") = "Area"
                            Flag = dtTabl.Rows(i)("cmm_Category")
                        End If
                        dr("Name") = dtTabl.Rows(i)("cmm_Desc")
                        dr("Code") = dtTabl.Rows(i)("cmm_Code")
                        dr("Remarks") = dtTabl.Rows(i)("cms_Remarks")
                        If IsDBNull(dtTabl.Rows(i)("CMM_CrBy")) = False Then
                            dr("Created by") = dtTabl.Rows(i)("CMM_CrBy")
                        End If
                        If IsDBNull(dtTabl.Rows(i)("CMM_CrOn")) = False Then
                            dr("Created on") = dtTabl.Rows(i)("CMM_CrOn")
                        End If
                        If IsDBNull(dtTabl.Rows(i)("CMM_ApprovedBy")) = False Then
                            dr("Approved by") = dtTabl.Rows(i)("CMM_ApprovedBy")
                        End If
                        If IsDBNull(dtTabl.Rows(i)("CMM_ApprovedOn")) = False Then
                            dr("Approved on") = dtTabl.Rows(i)("CMM_ApprovedOn")
                        End If
                        dt.Rows.Add(dr)
                    End If
                End If
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSysBCMMethodologyReports(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Dim dtTabl As New DataTable, dt As New DataTable
        Dim dr As DataRow
        Dim Flag As String = ""
        Try
            dt.Columns.Add("Master")
            dt.Columns.Add("Name")
            dt.Columns.Add("Code")
            dt.Columns.Add("Remarks")
            dt.Columns.Add("Created by")
            dt.Columns.Add("Created on")
            dt.Columns.Add("Approved by")
            dt.Columns.Add("Approved on")
            sSql = "Select a.cmm_Category,a.cmm_Desc,a.cmm_Code,a.cms_Remarks,convert(char(10),a.CMM_CrOn,103) CMM_CrOn,convert(char(10),a.CMM_ApprovedOn,103) CMM_ApprovedOn,"
            sSql = sSql & " b.usr_FullName As CMM_CrBy,c.usr_FullName As CMM_ApprovedBy from Content_Management_Master a Left join Sad_UserDetails b  On a.CMM_CrBy=b.usr_Id "
            sSql = sSql & " Left join Sad_UserDetails c  on a.CMM_ApprovedBy=c.usr_Id Where CMM_CompID= " & iACID & " order by cmm_Desc"
            dtTabl = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtTabl.Rows.Count - 1
                dr = dt.NewRow
                If IsDBNull(dtTabl.Rows(i)("cmm_Category")) = False Then
                    If (dtTabl.Rows(i)("cmm_Category") = "M ") Then
                        If (dtTabl.Rows(i)("cmm_Category") = Flag) Then
                            dr("Master") = ""
                        Else
                            dr("Master") = "Methodology"
                            Flag = dtTabl.Rows(i)("cmm_Category")
                        End If
                        dr("Name") = dtTabl.Rows(i)("cmm_Desc")
                        dr("Code") = dtTabl.Rows(i)("cmm_Code")
                        dr("Remarks") = dtTabl.Rows(i)("cms_Remarks")
                        If IsDBNull(dtTabl.Rows(i)("CMM_CrBy")) = False Then
                            dr("Created by") = dtTabl.Rows(i)("CMM_CrBy")
                        End If
                        If IsDBNull(dtTabl.Rows(i)("CMM_CrOn")) = False Then
                            dr("Created on") = dtTabl.Rows(i)("CMM_CrOn")
                        End If
                        If IsDBNull(dtTabl.Rows(i)("CMM_ApprovedBy")) = False Then
                            dr("Approved by") = dtTabl.Rows(i)("CMM_ApprovedBy")
                        End If
                        If IsDBNull(dtTabl.Rows(i)("CMM_ApprovedOn")) = False Then
                            dr("Approved on") = dtTabl.Rows(i)("CMM_ApprovedOn")
                        End If
                        dt.Rows.Add(dr)
                    End If
                End If
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSysBCMSamplesizeReports(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Dim dtTabl As New DataTable, dt As New DataTable
        Dim dr As DataRow
        Dim Flag As String = ""
        Try
            dt.Columns.Add("Master")
            dt.Columns.Add("Name")
            dt.Columns.Add("Code")
            dt.Columns.Add("Remarks")
            dt.Columns.Add("Created by")
            dt.Columns.Add("Created on")
            dt.Columns.Add("Approved by")
            dt.Columns.Add("Approved on")
            sSql = "Select a.cmm_Category,a.cmm_Desc,a.cmm_Code,a.cms_Remarks,convert(char(10),a.CMM_CrOn,103) CMM_CrOn,convert(char(10),a.CMM_ApprovedOn,103) CMM_ApprovedOn,"
            sSql = sSql & " b.usr_FullName As CMM_CrBy,c.usr_FullName As CMM_ApprovedBy from Content_Management_Master a Left join Sad_UserDetails b  On a.CMM_CrBy=b.usr_Id "
            sSql = sSql & " Left join Sad_UserDetails c  on a.CMM_ApprovedBy=c.usr_Id Where CMM_CompID= " & iACID & " order by cmm_Desc"
            dtTabl = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtTabl.Rows.Count - 1
                dr = dt.NewRow
                If IsDBNull(dtTabl.Rows(i)("cmm_Category")) = False Then
                    If (dtTabl.Rows(i)("cmm_Category") = "SS") Then
                        If (dtTabl.Rows(i)("cmm_Category") = Flag) Then
                            dr("Master") = ""
                        Else
                            dr("Master") = "Sample size"
                            Flag = dtTabl.Rows(i)("cmm_Category")
                        End If
                        dr("Name") = dtTabl.Rows(i)("cmm_Desc")
                        dr("Code") = dtTabl.Rows(i)("cmm_Code")
                        dr("Remarks") = dtTabl.Rows(i)("cms_Remarks")
                        If IsDBNull(dtTabl.Rows(i)("CMM_CrBy")) = False Then
                            dr("Created by") = dtTabl.Rows(i)("CMM_CrBy")
                        End If
                        If IsDBNull(dtTabl.Rows(i)("CMM_CrOn")) = False Then
                            dr("Created on") = dtTabl.Rows(i)("CMM_CrOn")
                        End If
                        If IsDBNull(dtTabl.Rows(i)("CMM_ApprovedBy")) = False Then
                            dr("Approved by") = dtTabl.Rows(i)("CMM_ApprovedBy")
                        End If
                        If IsDBNull(dtTabl.Rows(i)("CMM_ApprovedOn")) = False Then
                            dr("Approved on") = dtTabl.Rows(i)("CMM_ApprovedOn")
                        End If
                        dt.Rows.Add(dr)
                    End If
                End If
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSysBCMMasterReports(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal sMasterType As String) As DataTable
        Dim sSql As String
        Dim dtDetails As New DataTable, dt As New DataTable
        Dim dRow As DataRow
        Dim Flag As String = ""
        Dim dr As DataRow
        Try
            dt.Columns.Add("Master")
            dt.Columns.Add("Name")
            dt.Columns.Add("Code")
            dt.Columns.Add("Remarks")
            dt.Columns.Add("Start value")
            dt.Columns.Add("End value")
            dt.Columns.Add("Color")
            dt.Columns.Add("Created by")
            dt.Columns.Add("Created on")
            dt.Columns.Add("Approved by")
            dt.Columns.Add("Approved on")
            sSql = "Select a.cmm_Category,a.cmm_Desc,a.cmm_Code,a.cms_Remarks,convert(char(10),a.CMM_CrOn,103) CMM_CrOn,convert(char(10),a.CMM_ApprovedOn,103) CMM_ApprovedOn,"
            sSql = sSql & " b.usr_FullName As CMM_CrBy,c.usr_FullName As CMM_ApprovedBy from Content_Management_Master a Left join Sad_UserDetails b  On a.CMM_CrBy=b.usr_Id "
            sSql = sSql & " Left join Sad_UserDetails c  on a.CMM_ApprovedBy=c.usr_Id Where CMM_CompID= " & iACID & " order by cmm_Category"
            dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                If (dtDetails.Rows(i)("cmm_Category") = "AR") Then
                    If (dtDetails.Rows(i)("cmm_Category") = Flag) Then
                        dRow("Master") = ""
                    Else
                        dRow("Master") = "Area"
                        Flag = dtDetails.Rows(i)("cmm_Category")
                    End If
                    GoTo Description
                End If
                If (dtDetails.Rows(i)("cmm_Category") = "M ") Then
                    If (dtDetails.Rows(i)("cmm_Category") = Flag) Then
                        dRow("Master") = ""
                    Else
                        dr = dt.NewRow()
                        dt.Rows.Add(dr)
                        dr = dt.NewRow()
                        dt.Rows.Add(dr)
                        dRow("Master") = "Methodology"
                        Flag = dtDetails.Rows(i)("cmm_Category")
                    End If
                    GoTo Description
                End If
                If (dtDetails.Rows(i)("cmm_Category") = "SS") Then
                    If (dtDetails.Rows(i)("cmm_Category") = Flag) Then
                        dRow("Master") = ""
                    Else
                        dr = dt.NewRow()
                        dt.Rows.Add(dr)
                        dr = dt.NewRow()
                        dt.Rows.Add(dr)
                        dRow("Master") = "Sample size"
                        Flag = dtDetails.Rows(i)("cmm_Category")
                    End If
                    GoTo Description
                End If
Description:    If IsDBNull(dtDetails.Rows(i)("cmm_Desc")) = False Then
                    dRow("Name") = dtDetails.Rows(i)("cmm_Desc")
                End If
                If IsDBNull(dtDetails.Rows(i)("cmm_Code")) = False Then
                    dRow("Code") = dtDetails.Rows(i)("cmm_Code")
                End If
                If IsDBNull(dtDetails.Rows(i)("cms_Remarks")) = False Then
                    dRow("Remarks") = dtDetails.Rows(i)("cms_Remarks")
                End If
                If IsDBNull(dtDetails.Rows(i)("cmm_Category")) = True Then
                    dRow("Start value") = ""
                End If
                If IsDBNull(dtDetails.Rows(i)("cmm_Category")) = True Then
                    dRow("End value") = ""
                End If
                If IsDBNull(dtDetails.Rows(i)("cmm_Category")) = True Then
                    dRow("Color") = ""
                End If
                If IsDBNull(dtDetails.Rows(i)("CMM_CrBy")) = False Then
                    dRow("Created by") = dtDetails.Rows(i)("CMM_CrBy")
                End If
                If IsDBNull(dtDetails.Rows(i)("CMM_CrOn")) = False Then
                    dRow("Created on") = dtDetails.Rows(i)("CMM_CrOn")
                End If
                If IsDBNull(dtDetails.Rows(i)("CMM_ApprovedBy")) = False Then
                    dRow("Approved by") = dtDetails.Rows(i)("CMM_ApprovedBy")
                End If
                If IsDBNull(dtDetails.Rows(i)("CMM_ApprovedOn")) = False Then
                    dRow("Approved on") = dtDetails.Rows(i)("CMM_ApprovedOn")
                End If
                dt.Rows.Add(dRow)
            Next

            sSql = "" : sSql = " select a.CMAR_Name,a.CMAR_Desc,a.CMAR_StartValue,a.CMAR_EndValue,a.CMAR_Color,convert(char(10),a.CMAR_CrOn,103) CMAR_CrOn,convert(char(10),a.CMAR_ApprovedOn,103) CMAR_ApprovedOn,"
            sSql = sSql & " c.usr_FullName As CMAR_ApprovedBy,b.usr_FullName As CMAR_CrBy from CMARating a Left join Sad_UserDetails b  On a.CMAR_CrBy=b.usr_Id "
            sSql = sSql & " Left join Sad_UserDetails c  on a.CMAR_ApprovedBy=c.usr_Id where CMAR_CompID= " & iACID & " And CMAR_YearID=" & iYearID & " order by CMAR_Name"
            dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                If IsDBNull(dtDetails.Rows(i)("CMAR_Name")) = False Then
                    If Flag.Contains("BCM Rating") Then
                        dRow("Master") = ""
                    Else
                        dr = dt.NewRow()
                        dt.Rows.Add(dr)
                        dr = dt.NewRow()
                        dt.Rows.Add(dr)
                        dRow("Master") = "BCM Rating"
                        Flag = dRow("Master")
                    End If
                    dRow("Name") = dtDetails.Rows(i)("CMAR_Name")
                    If IsDBNull(dtDetails.Rows(i)("CMAR_Desc")) = False Then
                        dRow("Code") = ""
                        dRow("Remarks") = dtDetails.Rows(i)("CMAR_Desc")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("CMAR_StartValue")) = False Then
                        dRow("Start value") = dtDetails.Rows(i)("CMAR_StartValue")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("CMAR_EndValue")) = False Then
                        dRow("End value") = dtDetails.Rows(i)("CMAR_EndValue")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("CMAR_Color")) = False Then
                        dRow("Color") = dtDetails.Rows(i)("CMAR_Color")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("CMAR_CrBy")) = False Then
                        dRow("Created by") = dtDetails.Rows(i)("CMAR_CrBy")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("CMAR_CrOn")) = False Then
                        dRow("Created on") = dtDetails.Rows(i)("CMAR_CrOn")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("CMAR_ApprovedBy")) = False Then
                        dRow("Approved by") = dtDetails.Rows(i)("CMAR_ApprovedBy")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("CMAR_ApprovedOn")) = False Then
                        dRow("Approved on") = dtDetails.Rows(i)("CMAR_ApprovedOn")
                    End If
                    dt.Rows.Add(dRow)
                End If
            Next

            sSql = "" : sSql = " select a.CMASR_Name,a.CMASR_Desc,a.CMASR_StartValue,a.CMASR_EndValue,a.CMASR_Color,convert(char(10),a.CMASR_CrOn,103) CMASR_CrOn,convert(char(10),a.CMASR_ApprovedOn,103) CMASR_ApprovedOn,"
            sSql = sSql & " c.usr_FullName As CMASR_ApprovedBy,b.usr_FullName As CMASR_CrBy from CMARating_SupportProcess a Left join Sad_UserDetails b  On a.CMASR_CrBy=b.usr_Id "
            sSql = sSql & " Left join Sad_UserDetails c  on a.CMASR_ApprovedBy=c.usr_Id  where CMASR_CompID= " & iACID & " And CMASR_YearID=" & iYearID & " order by CMASR_Name"
            dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                If IsDBNull(dtDetails.Rows(i)("CMASR_Name")) = False Then
                    If Flag.Contains("BCM Support Process Rating") Then
                        dRow("Master") = ""
                    Else
                        dr = dt.NewRow()
                        dt.Rows.Add(dr)
                        dr = dt.NewRow()
                        dt.Rows.Add(dr)
                        dRow("Master") = "BCM Support Process Rating"
                        Flag = dRow("Master")
                    End If
                    dRow("Name") = dtDetails.Rows(i)("CMASR_Name")
                    If IsDBNull(dtDetails.Rows(i)("CMASR_Desc")) = False Then
                        dRow("Code") = ""
                        dRow("Remarks") = dtDetails.Rows(i)("CMASR_Desc")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("CMASR_StartValue")) = False Then
                        dRow("Start value") = dtDetails.Rows(i)("CMASR_StartValue")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("CMASR_EndValue")) = False Then
                        dRow("End value") = dtDetails.Rows(i)("CMASR_EndValue")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("CMASR_Color")) = False Then
                        dRow("Color") = dtDetails.Rows(i)("CMASR_Color")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("CMASR_CrBy")) = False Then
                        dRow("Created by") = dtDetails.Rows(i)("CMASR_CrBy")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("CMASR_CrOn")) = False Then
                        dRow("Created on") = dtDetails.Rows(i)("CMASR_CrOn")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("CMASR_ApprovedBy")) = False Then
                        dRow("Approved by") = dtDetails.Rows(i)("CMASR_ApprovedBy")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("CMASR_ApprovedOn")) = False Then
                        dRow("Approved on") = dtDetails.Rows(i)("CMASR_ApprovedOn")
                    End If
                    dt.Rows.Add(dRow)
                End If
            Next

            sSql = "" : sSql = "Select a.CMACR_Name,a.CMACR_Desc,a.CMACR_StartValue,a.CMACR_EndValue,a.CMACR_Color,convert(char(10),a.CMACR_CrOn,103) CMACR_CrOn,convert(char(10),a.CMACR_ApprovedOn,103) CMACR_ApprovedOn,"
            sSql = sSql & " c.usr_FullName As CMACR_ApprovedBy,b.usr_FullName As CMACR_CrBy from CMARating_CoreProcess a Left join Sad_UserDetails b  On a.CMACR_CrBy=b.usr_Id "
            sSql = sSql & " Left join Sad_UserDetails c  on a.CMACR_ApprovedBy=c.usr_Id  where CMACR_CompID= " & iACID & " And CMACR_YearID=" & iYearID & " order by CMACR_Name"
            dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                If IsDBNull(dtDetails.Rows(i)("CMACR_Name")) = False Then
                    If Flag.Contains("BCM Core Process Rating") Then
                        dRow("Master") = ""
                    Else
                        dr = dt.NewRow()
                        dt.Rows.Add(dr)
                        dr = dt.NewRow()
                        dt.Rows.Add(dr)

                        dRow("Master") = "BCM Core Process Rating"
                        Flag = dRow("Master")
                    End If
                    dRow("Name") = dtDetails.Rows(i)("CMACR_Name")
                    If IsDBNull(dtDetails.Rows(i)("CMACR_Desc")) = False Then
                        dRow("Code") = ""
                        dRow("Remarks") = dtDetails.Rows(i)("CMACR_Desc")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("CMACR_StartValue")) = False Then
                        dRow("Start value") = dtDetails.Rows(i)("CMACR_StartValue")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("CMACR_EndValue")) = False Then
                        dRow("End value") = dtDetails.Rows(i)("CMACR_EndValue")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("CMACR_Color")) = False Then
                        dRow("Color") = dtDetails.Rows(i)("CMACR_Color")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("CMACR_CrBy")) = False Then
                        dRow("Created by") = dtDetails.Rows(i)("CMACR_CrBy")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("CMACR_CrOn")) = False Then
                        dRow("Created on") = dtDetails.Rows(i)("CMACR_CrOn")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("CMACR_ApprovedBy")) = False Then
                        dRow("Approved by") = dtDetails.Rows(i)("CMACR_ApprovedBy")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("CMACR_ApprovedOn")) = False Then
                        dRow("Approved on") = dtDetails.Rows(i)("CMACR_ApprovedOn")
                    End If
                    dt.Rows.Add(dRow)
                End If
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

End Class
