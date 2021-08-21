Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class clsVehicleLoanMaster
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions

    Private iLVLM_ID As Integer
    Private iLVLM_MasterID As Integer
    Private sLVLM_RegNo As String
    Private dLVLM_LoanAmount As Double
    Private sLVLM_LoanAccNo As String
    Private iLVLM_BankName As Integer
    Private sLVLM_BranchName As String
    Private dLVLM_LoanDate As Date
    Private dLVLM_LoanDueDate As Date
    Private dLVLM_InstallmentAmt As Double

    Private sLVLM_Delflag As String
    Private iLVLM_CompID As Integer
    Private iLVLM_YearID As Integer
    Private sLVLM_Status As String
    Private sLVLM_Operation As String
    Private sLVLM_IPAddress As String
    Private iLVLM_CreatedBy As Integer
    Private dLVLM_CreatedOn As DateTime
    Private iLVLM_ApprovedBy As Integer
    Private dLVLM_ApprovedOn As DateTime
    Private iLVLM_DeletedBy As Integer
    Private dLVLM_DeletedOn As DateTime
    Private iLVLM_UpdatedBy As Integer
    Private dLVLM_UpdatedOn As DateTime
    Private iLVLM_RecalldBy As Integer
    Public Property LVLM_ID() As Integer
        Get
            Return (iLVLM_ID)
        End Get
        Set(ByVal Value As Integer)
            iLVLM_ID = Value
        End Set
    End Property
    Public Property LVLM_MasterID() As Integer
        Get
            Return (iLVLM_MasterID)
        End Get
        Set(ByVal Value As Integer)
            iLVLM_MasterID = Value
        End Set
    End Property

    Public Property LVLM_RegNo() As String
        Get
            Return (sLVLM_RegNo)
        End Get
        Set(ByVal Value As String)
            sLVLM_RegNo = Value
        End Set
    End Property
    Public Property LVLM_LoanAmount() As Double
        Get
            Return (dLVLM_LoanAmount)
        End Get
        Set(ByVal Value As Double)
            dLVLM_LoanAmount = Value
        End Set
    End Property
    Public Property LVLM_LoanAccNo() As String
        Get
            Return (sLVLM_LoanAccNo)
        End Get
        Set(ByVal Value As String)
            sLVLM_LoanAccNo = Value
        End Set
    End Property
    Public Property LVLM_BankName() As Integer
        Get
            Return (iLVLM_BankName)
        End Get
        Set(ByVal Value As Integer)
            iLVLM_BankName = Value
        End Set
    End Property
    Public Property LVLM_BranchName() As String
        Get
            Return (sLVLM_BranchName)
        End Get
        Set(ByVal Value As String)
            sLVLM_BranchName = Value
        End Set
    End Property
    Public Property LVLM_LoanDate() As Date
        Get
            Return (dLVLM_LoanDate)
        End Get
        Set(ByVal Value As Date)
            dLVLM_LoanDate = Value
        End Set
    End Property
    Public Property LVLM_LoanDueDate() As Date
        Get
            Return (dLVLM_LoanDueDate)
        End Get
        Set(ByVal Value As Date)
            dLVLM_LoanDueDate = Value
        End Set
    End Property
    Public Property LVLM_InstallmentAmt() As Double
        Get
            Return (dLVLM_InstallmentAmt)
        End Get
        Set(ByVal Value As Double)
            dLVLM_InstallmentAmt = Value
        End Set
    End Property
    Public Property LVLM_Delflag() As String
        Get
            Return (sLVLM_Delflag)
        End Get
        Set(ByVal Value As String)
            sLVLM_Delflag = Value
        End Set
    End Property
    Public Property LVLM_CompID() As Integer
        Get
            Return (iLVLM_CompID)
        End Get
        Set(ByVal Value As Integer)
            iLVLM_CompID = Value
        End Set
    End Property
    Public Property LVLM_YearID() As Integer
        Get
            Return (iLVLM_YearID)
        End Get
        Set(ByVal Value As Integer)
            iLVLM_YearID = Value
        End Set
    End Property
    Public Property LVLM_Status() As String
        Get
            Return (sLVLM_Status)
        End Get
        Set(ByVal Value As String)
            sLVLM_Status = Value
        End Set
    End Property
    Public Property LVLM_Operation() As String
        Get
            Return (sLVLM_Operation)
        End Get
        Set(ByVal Value As String)
            sLVLM_Operation = Value
        End Set
    End Property
    Public Property LVLM_IPAddress() As String
        Get
            Return (sLVLM_IPAddress)
        End Get
        Set(ByVal Value As String)
            sLVLM_IPAddress = Value
        End Set
    End Property
    Public Property LVLM_CreatedBy() As Integer
        Get
            Return (iLVLM_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iLVLM_CreatedBy = Value
        End Set
    End Property
    Public Property LVLM_CreatedOn() As Date
        Get
            Return (dLVLM_CreatedOn)
        End Get
        Set(ByVal Value As Date)
            dLVLM_CreatedOn = Value
        End Set
    End Property

    Public Property LVLM_ApprovedBy() As Integer
        Get
            Return (iLVLM_ApprovedBy)
        End Get
        Set(ByVal Value As Integer)
            iLVLM_ApprovedBy = Value
        End Set
    End Property
    Public Property LVLM_ApprovedOn() As Date
        Get
            Return (dLVLM_ApprovedOn)
        End Get
        Set(ByVal Value As Date)
            dLVLM_ApprovedOn = Value
        End Set
    End Property
    Public Property LVLM_DeletedBy() As Integer
        Get
            Return (iLVLM_DeletedBy)
        End Get
        Set(ByVal Value As Integer)
            iLVLM_DeletedBy = Value
        End Set
    End Property
    Public Property LVLM_DeletedOn() As Date
        Get
            Return (dLVLM_DeletedOn)
        End Get
        Set(ByVal Value As Date)
            dLVLM_DeletedOn = Value
        End Set
    End Property
    Public Property LVLM_UpdatedBy() As Integer
        Get
            Return (iLVLM_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iLVLM_UpdatedBy = Value
        End Set
    End Property
    Public Property LVLM_UpdatedOn() As Date
        Get
            Return (dLVLM_UpdatedOn)
        End Get
        Set(ByVal Value As Date)
            dLVLM_UpdatedOn = Value
        End Set
    End Property
    Public Property LVLM_RecalldBy() As Integer
        Get
            Return (iLVLM_RecalldBy)
        End Get
        Set(ByVal Value As Integer)
            iLVLM_RecalldBy = Value
        End Set
    End Property

    Private iLVLD_ID As Integer
    Private iLVLD_MasterID As Integer
    Private sLVLD_RegNo As String
    Private dLVLD_LoanInsDueDate As Date
    Private dLVLD_InstallmentPaidDt As Date
    Private dLVLD_InstallmentPaidAmt As Double
    Private dLVLD_InstallmentInterestAmt As Double
    Private dLVLD_TotalAmt As Double
    Private sLVLD_Reference As String
    Private sLVLD_DelFlag As String
    Private sLVLD_Status As String
    Private iLVLD_CreatedBy As Integer
    Private dLVLD_CreatedOn As Date
    Private iLVLD_UpdatedBy As Integer
    Private dLVLD_UpdatedOn As Date
    Private iLVLD_CompID As Integer
    Private iLVLD_YearID As Integer
    Private sLVLD_Operation As String
    Private sLVLD_IPAddress As String




    Public Property LVLD_ID() As Integer
        Get
            Return (iLVLD_ID)
        End Get
        Set(ByVal Value As Integer)
            iLVLD_ID = Value
        End Set
    End Property
    Public Property LVLD_MasterID() As Integer
        Get
            Return (iLVLD_MasterID)
        End Get
        Set(ByVal Value As Integer)
            iLVLD_MasterID = Value
        End Set
    End Property
    Public Property LVLD_RegNo() As String
        Get
            Return (sLVLD_RegNo)
        End Get
        Set(ByVal Value As String)
            sLVLD_RegNo = Value
        End Set
    End Property
    Public Property LVLD_LoanInsDueDate() As Date
        Get
            Return (dLVLD_LoanInsDueDate)
        End Get
        Set(ByVal Value As Date)
            dLVLD_LoanInsDueDate = Value
        End Set
    End Property
    Public Property LVLD_InstallmentPaidDt() As Date
        Get
            Return (dLVLD_InstallmentPaidDt)
        End Get
        Set(ByVal Value As Date)
            dLVLD_InstallmentPaidDt = Value
        End Set
    End Property
    Public Property LVLD_InstallmentPaidAmt() As Double
        Get
            Return (dLVLD_InstallmentPaidAmt)
        End Get
        Set(ByVal Value As Double)
            dLVLD_InstallmentPaidAmt = Value
        End Set
    End Property
    Public Property LVLD_InstallmentInterestAmt() As Double
        Get
            Return (dLVLD_InstallmentInterestAmt)
        End Get
        Set(ByVal Value As Double)
            dLVLD_InstallmentInterestAmt = Value
        End Set
    End Property

    Public Property LVLD_TotalAmt() As Double
        Get
            Return (dLVLD_TotalAmt)
        End Get
        Set(ByVal Value As Double)
            dLVLD_TotalAmt = Value
        End Set
    End Property
    Public Property LVLD_Reference() As String
        Get
            Return (sLVLD_Reference)
        End Get
        Set(ByVal Value As String)
            sLVLD_Reference = Value
        End Set
    End Property
    Public Property LVLD_DelFlag() As String
        Get
            Return (sLVLD_DelFlag)
        End Get
        Set(ByVal Value As String)
            sLVLD_DelFlag = Value
        End Set
    End Property
    Public Property LVLD_Status() As String
        Get
            Return (sLVLD_Status)
        End Get
        Set(ByVal Value As String)
            sLVLD_Status = Value
        End Set
    End Property
    Public Property LVLD_CreatedBy() As Integer
        Get
            Return (iLVLD_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iLVLD_CreatedBy = Value
        End Set
    End Property
    Public Property LVLD_CreatedOn() As DateTime
        Get
            Return (dLVLD_CreatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dLVLD_CreatedOn = Value
        End Set
    End Property
    Public Property LVLD_UpdatedBy() As Integer
        Get
            Return (iLVLD_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iLVLD_UpdatedBy = Value
        End Set
    End Property
    Public Property LVLD_UpdatedOn() As DateTime
        Get
            Return (dLVLD_UpdatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dLVLD_UpdatedOn = Value
        End Set
    End Property

    Public Property LVLD_CompID() As Integer
        Get
            Return (iLVLD_CompID)
        End Get
        Set(ByVal Value As Integer)
            iLVLD_CompID = Value
        End Set
    End Property
    Public Property LVLD_YearID() As Integer
        Get
            Return (iLVLD_YearID)
        End Get
        Set(ByVal Value As Integer)
            iLVLD_YearID = Value
        End Set
    End Property
    Public Property LVLD_Operation() As String
        Get
            Return (sLVLD_Operation)
        End Get
        Set(ByVal Value As String)
            sLVLD_Operation = Value
        End Set
    End Property
    Public Property LVLD_IPAddress() As String
        Get
            Return (sLVLD_IPAddress)
        End Get
        Set(ByVal Value As String)
            sLVLD_IPAddress = Value
        End Set
    End Property

    Public Function SaveLoanMater(ByVal sNameSpace As String, ByVal objLVLM As clsVehicleLoanMaster) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(21) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVLM_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLVLM.iLVLM_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVLM_MasterID ", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLVLM.iLVLM_MasterID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVLM_RegNo ", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objLVLM.sLVLM_RegNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVLM_LoanAmount ", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objLVLM.dLVLM_LoanAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVLM_LoanAccNo ", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objLVLM.sLVLM_LoanAccNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVLM_BankName ", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objLVLM.iLVLM_BankName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVLM_BranchName ", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objLVLM.sLVLM_BranchName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVLM_LoanDate ", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objLVLM.dLVLM_LoanDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVLM_LoanDueDate ", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objLVLM.dLVLM_LoanDueDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVLM_InstallmentAmt ", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objLVLM.dLVLM_InstallmentAmt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVLM_Delflag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objLVLM.sLVLM_Delflag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVLM_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objLVLM.sLVLM_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVLM_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLVLM.iLVLM_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVLM_CreatedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objLVLM.dLVLM_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVLM_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLVLM.iLVLM_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVLM_UpdatedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objLVLM.dLVLM_UpdatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVLM_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLVLM.iLVLM_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVLM_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLVLM.iLVLM_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVLM_Operation", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objLVLM.sLVLM_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVLM_IPAddress", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objLVLM.sLVLM_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spLgst_Vehicle_LoanMaster", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveLoanDetails(ByVal sNameSpace As String, ByVal objLVLD As clsVehicleLoanMaster) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(20) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVLD_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLVLD.iLVLD_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVLD_MasterID ", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLVLD.iLVLD_MasterID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVLD_RegNo ", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objLVLD.sLVLD_RegNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVLD_LoanInsDueDate ", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objLVLD.dLVLD_LoanInsDueDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVLD_InstallmentPaidDt ", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objLVLD.dLVLD_InstallmentPaidDt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVLD_InstallmentPaidAmt ", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objLVLD.dLVLD_InstallmentPaidAmt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVLD_InstallmentInterestAmt ", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objLVLD.dLVLD_InstallmentInterestAmt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVLD_TotalAmt ", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objLVLD.dLVLD_TotalAmt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVLD_Reference ", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objLVLD.sLVLD_Reference
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVLD_Delflag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objLVLD.sLVLD_DelFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVLD_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objLVLD.sLVLD_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVLD_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLVLD.iLVLD_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVLD_CreatedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objLVLD.dLVLD_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVLD_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLVLD.iLVLD_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVLD_UpdatedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objLVLD.dLVLD_UpdatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVLD_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLVLD.iLVLD_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVLD_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLVLD.iLVLD_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVLD_Operation", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objLVLD.sLVLD_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LVLD_IPAddress", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objLVLD.sLVLD_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spLgst_Vehicle_LoanDetails", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadVehicleDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCSMid As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select * from Lgst_Vehicle_LoanMaster Where LVLM_MasterID=" & iCSMid & "  And LVLM_CompID=" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadVehicle(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select LVAM_ID,LVAM_RegNo From Lgst_Vehicle_AdditionalMaster Where LVAM_CompID=" & iCompID & "" ' and lvm_id in (select LVLM_VehivleNo from Lgst_TripGeneration_Master where LVLM_TripStatus =1 and LVLM_compid= " & iCompID & ") "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadLoanVehicle(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select LVLM_MasterID,LVLM_RegNo From Lgst_Vehicle_LoanMaster Where LVLM_CompID=" & iCompID & "" ' and lvm_id in (select LVLM_VehivleNo from Lgst_TripGeneration_Master where LVLM_TripStatus =1 and LVLM_compid= " & iCompID & ") "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadBanksName(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim iAcc_GL As New Integer

        Try
            sSql = "select Acc_GL From Acc_Application_Settings Where Acc_Types='Bank' And Acc_LedgerType='Bank'"
            iAcc_GL = objDBL.SQLExecuteScalar(sNameSpace, sSql)

            sSql = "Select gl_id, (gl_glCode + ' - ' + gl_desc) as Description from Chart_Of_Accounts where gl_parent=" & iAcc_GL & " and gl_CompId=" & iCompID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadVehicleLoanDashDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iStatus As Integer) As DataTable
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dRow As DataRow
        Dim sSql As String = ""
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("LVLM_ID")
            dt.Columns.Add("RegistrationNo")
            dt.Columns.Add("AccNo")
            dt.Columns.Add("LoanAmt")
            dt.Columns.Add("InstallmentAmt")
            dt.Columns.Add("Status")


            sSql = "select * from Lgst_Vehicle_LoanMaster where LVLM_CompID=" & iCompID & ""
            'If iStatus = 0 Then
            '    sSql = sSql & " And LVLM_DelFlag ='A'" 'Activated
            'ElseIf iStatus = 1 Then
            '    sSql = sSql & " And LVLM_DelFlag='D'" 'De-Activated
            'ElseIf iStatus = 2 Then
            '    sSql = sSql & " And LVLM_DelFlag='W'" 'Waiting for approval
            'End If
            dtDetails = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                dRow("SrNo") = i + 1
                If IsDBNull(dtDetails.Rows(i)("LVLM_MasterID")) = False Then
                    dRow("LVLM_ID") = dtDetails.Rows(i)("LVLM_MasterID")
                End If
                If IsDBNull(dtDetails.Rows(i)("LVLM_RegNo")) = False Then
                    dRow("RegistrationNo") = dtDetails.Rows(i)("LVLM_RegNo")
                End If
                If IsDBNull(dtDetails.Rows(i)("LVLM_LoanAccNo")) = False Then
                    dRow("AccNo") = dtDetails.Rows(i)("LVLM_LoanAccNo")
                End If
                If IsDBNull(dtDetails.Rows(i)("LVLM_LoanAmount")) = False Then
                    dRow("LoanAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(0).Item("LVLM_LoanAmount").ToString()))
                End If
                If IsDBNull(dtDetails.Rows(i)("LVLM_InstallmentAmt")) = False Then
                    dRow("InstallmentAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(0).Item("LVLM_InstallmentAmt").ToString()))
                End If
                If dtDetails.Rows(i)("LVLM_Status") = "C" Then
                    dRow("Status") = "Waiting For Approval"
                ElseIf dtDetails.Rows(i)("LVLM_Status") = "D" Then
                    dRow("Status") = "Deleted"
                ElseIf dtDetails.Rows(i)("LVLM_Status") = "A" Then
                    dRow("Status") = "Activated"
                End If
                dt.Rows.Add(dRow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadInstallmentDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iVehId As Integer, ByVal iYearId As Integer) As DataTable
        Dim sSql As String
        Dim dr As OleDb.OleDbDataReader
        Dim dt As New DataTable
        Dim dRow As DataRow
        Try
            dt.Columns.Add("InstallID")
            dt.Columns.Add("DueDate")
            dt.Columns.Add("PaymentDate")
            dt.Columns.Add("InstallmentAmt")
            dt.Columns.Add("InterestAmt")
            dt.Columns.Add("TotalAmt")
            dt.Columns.Add("Reference")

            sSql = "Select * from Lgst_Vehicle_LoanDetails Where LVLD_DelFlag<>'D' And LVLD_MasterID=" & iVehId & " And LVLD_CompID=" & iCompID & "   Order by LVLD_ID"
            dr = objDBL.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows Then
                While dr.Read
                    dRow = dt.NewRow
                    dRow("InstallID") = dr("LVLD_ID")
                    dRow("DueDate") = objGen.FormatDtForRDBMS(dr("LVLD_LoanInsDueDate").ToString(), "D")
                    dRow("PaymentDate") = objGen.FormatDtForRDBMS(dr("LVLD_InstallmentPaidDt").ToString(), "D")
                    dRow("InstallmentAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dr("LVLD_InstallmentPaidAmt").ToString()))
                    dRow("InterestAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dr("LVLD_InstallmentInterestAmt").ToString()))
                    dRow("TotalAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dr("LVLD_TotalAmt").ToString()))
                    dRow("Reference") = dr("LVLD_Reference")
                    dt.Rows.Add(dRow)
                End While
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetLoanDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iID As Integer, ByVal iMasterID As Integer, ByVal iYearId As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * from Lgst_Vehicle_LoanDetails Where LVLD_DelFlag<>'D' And LVLD_ID=" & iID & " And LVLD_MasterID=" & iMasterID & " And LVLD_CompID=" & iCompID & " and LVLD_YearID= " & iYearId & " Order by LVLD_ID"
            GetLoanDetails = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return GetLoanDetails
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub DeleteLoanValue(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iID As Integer, ByVal iMasterID As Integer, ByVal iYearId As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Update Lgst_Vehicle_TyreMaster Set LVTM_DelFlag='D' Where LVTM_ID=" & iID & " And LVTM_MasterID=" & iMasterID & " And LVTM_CompID=" & iCompID & " and LVTM_YearID= " & iYearId & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    '******
    Public Function GetDebitAmt(ByVal sNameSpace As String, ByVal iCompID As Integer) As Double
        Dim sSql As String = ""
        Dim dDebitAmt As Double = 0.0
        Dim dtDetails As New DataTable
        Try
            sSql = " select gl_AccHead,gl_id from chart_of_Accounts where gl_desc= 'Loan And Advance'"
            dtDetails = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            sSql = "" : sSql = "select sum(ATD_Debit) as TotalDebit from acc_Transactions_Details where ATD_Head='" & dtDetails.Rows(0)("gl_AccHead").ToString() & "'  and atd_gl='" & dtDetails.Rows(0)("gl_id").ToString() & "' And ATD_DbOrCr=1 and ATD_CompID=" & iCompID & ""
            dDebitAmt = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return dDebitAmt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetInsuranceAmt(ByVal sNameSpace As String, ByVal iCompID As Integer) As Double
        Dim sSql As String = ""
        Dim dInsuranceAmt As Double = 0.0
        Try
            sSql = "" : sSql = "select sum(LVLD_TotalAmt) as totalAmt from Lgst_Vehicle_LoanDetails where LVLD_CompID=" & iCompID & ""
            dInsuranceAmt = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return dInsuranceAmt
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
