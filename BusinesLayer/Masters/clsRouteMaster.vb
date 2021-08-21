Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class clsRouteMaster
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions

    Private iLRM_ID As Integer
    Private iLRM_VehicleType As Integer
    Private iLRM_StartPlace As Integer
    Private iLRM_DestPlace As Integer
    Private sLRM_StartDestPlace As String
    Private dLRM_DistinKms As Double
    Private dLRM_Rate As Double
    Private dLRM_DriverAlnceAmt As Double
    Private dLRM_PetrolQty As Double
    Private sLRM_AllottedTime As String
    Public Property LRM_ID() As Integer
        Get
            Return (iLRM_ID)
        End Get
        Set(ByVal Value As Integer)
            iLRM_ID = Value
        End Set
    End Property
    Public Property LRM_VehicleType() As Integer
        Get
            Return (iLRM_VehicleType)
        End Get
        Set(ByVal Value As Integer)
            iLRM_VehicleType = Value
        End Set
    End Property
    Public Property LRM_StartPlace() As Integer
        Get
            Return (iLRM_StartPlace)
        End Get
        Set(ByVal Value As Integer)
            iLRM_StartPlace = Value
        End Set
    End Property
    Public Property LRM_DestPlace() As Integer
        Get
            Return (iLRM_DestPlace)
        End Get
        Set(ByVal Value As Integer)
            iLRM_DestPlace = Value
        End Set
    End Property

    Public Property LRM_StartDestPlace() As String
        Get
            Return (sLRM_StartDestPlace)
        End Get
        Set(ByVal Value As String)
            sLRM_StartDestPlace = Value
        End Set
    End Property
    Public Property LRM_DistinKms() As Double
        Get
            Return (dLRM_DistinKms)
        End Get
        Set(ByVal Value As Double)
            dLRM_DistinKms = Value
        End Set
    End Property
    Public Property LRM_Rate() As Double
        Get
            Return (dLRM_Rate)
        End Get
        Set(ByVal Value As Double)
            dLRM_Rate = Value
        End Set
    End Property
    Public Property LRM_DriverAlnceAmt() As Double
        Get
            Return (dLRM_DriverAlnceAmt)
        End Get
        Set(ByVal Value As Double)
            dLRM_DriverAlnceAmt = Value
        End Set
    End Property
    Public Property LRM_PetrolQty() As Double
        Get
            Return (dLRM_PetrolQty)
        End Get
        Set(ByVal Value As Double)
            dLRM_PetrolQty = Value
        End Set
    End Property
    Public Property LRM_AllottedTime() As Double
        Get
            Return (sLRM_AllottedTime)
        End Get
        Set(ByVal Value As Double)
            sLRM_AllottedTime = Value
        End Set
    End Property

    Private sLRM_Delflag As String
    Private iLRM_CompID As Integer
    Private iLRM_YearID As Integer
    Private sLRM_Status As String
    Private sLRM_Operation As String
    Private sLRM_IPAddress As String
    Private iLRM_CreatedBy As Integer
    Private dLRM_CreatedOn As DateTime
    Private iLRM_ApprovedBy As Integer
    Private dLRM_ApprovedOn As DateTime
    Private iLRM_DeletedBy As Integer
    Private dLRM_DeletedOn As DateTime
    Private iLRM_UpdatedBy As Integer
    Private dLRM_UpdatedOn As DateTime
    Private iLRM_RecalldBy As Integer

    Public Property LRM_Delflag() As String
        Get
            Return (sLRM_Delflag)
        End Get
        Set(ByVal Value As String)
            sLRM_Delflag = Value
        End Set
    End Property
    Public Property LRM_CompID() As Integer
        Get
            Return (iLRM_CompID)
        End Get
        Set(ByVal Value As Integer)
            iLRM_CompID = Value
        End Set
    End Property
    Public Property LRM_YearID() As Integer
        Get
            Return (iLRM_YearID)
        End Get
        Set(ByVal Value As Integer)
            iLRM_YearID = Value
        End Set
    End Property
    Public Property LRM_Status() As String
        Get
            Return (sLRM_Status)
        End Get
        Set(ByVal Value As String)
            sLRM_Status = Value
        End Set
    End Property
    Public Property LRM_Operation() As String
        Get
            Return (sLRM_Operation)
        End Get
        Set(ByVal Value As String)
            sLRM_Operation = Value
        End Set
    End Property
    Public Property LRM_IPAddress() As String
        Get
            Return (sLRM_IPAddress)
        End Get
        Set(ByVal Value As String)
            sLRM_IPAddress = Value
        End Set
    End Property
    Public Property LRM_CreatedBy() As Integer
        Get
            Return (iLRM_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iLRM_CreatedBy = Value
        End Set
    End Property
    Public Property LRM_CreatedOn() As Date
        Get
            Return (dLRM_CreatedOn)
        End Get
        Set(ByVal Value As Date)
            dLRM_CreatedOn = Value
        End Set
    End Property

    Public Property LRM_ApprovedBy() As Integer
        Get
            Return (iLRM_ApprovedBy)
        End Get
        Set(ByVal Value As Integer)
            iLRM_ApprovedBy = Value
        End Set
    End Property
    Public Property LRM_ApprovedOn() As Date
        Get
            Return (dLRM_ApprovedOn)
        End Get
        Set(ByVal Value As Date)
            dLRM_ApprovedOn = Value
        End Set
    End Property
    Public Property LRM_DeletedBy() As Integer
        Get
            Return (iLRM_DeletedBy)
        End Get
        Set(ByVal Value As Integer)
            iLRM_DeletedBy = Value
        End Set
    End Property
    Public Property LRM_DeletedOn() As Date
        Get
            Return (dLRM_DeletedOn)
        End Get
        Set(ByVal Value As Date)
            dLRM_DeletedOn = Value
        End Set
    End Property
    Public Property LRM_UpdatedBy() As Integer
        Get
            Return (iLRM_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iLRM_UpdatedBy = Value
        End Set
    End Property
    Public Property LRM_UpdatedOn() As Date
        Get
            Return (dLRM_UpdatedOn)
        End Get
        Set(ByVal Value As Date)
            dLRM_UpdatedOn = Value
        End Set
    End Property
    Public Property LRM_RecalldBy() As Integer
        Get
            Return (iLRM_RecalldBy)
        End Get
        Set(ByVal Value As Integer)
            iLRM_RecalldBy = Value
        End Set
    End Property

    Private iLRM_Group As Integer
    Private iLRM_SubGroup As Integer
    Private iLRM_GL As Integer
    Private iLRM_SubGL As Integer
    Public Property LRM_Group() As Integer
        Get
            Return (iLRM_Group)
        End Get
        Set(ByVal Value As Integer)
            iLRM_Group = Value
        End Set
    End Property
    Public Property LRM_SubGroup() As Integer
        Get
            Return (iLRM_SubGroup)
        End Get
        Set(ByVal Value As Integer)
            iLRM_SubGroup = Value
        End Set
    End Property
    Public Property LRM_GL() As Integer
        Get
            Return (iLRM_GL)
        End Get
        Set(ByVal Value As Integer)
            iLRM_GL = Value
        End Set
    End Property
    Public Property LRM_SubGL() As Integer
        Get
            Return (iLRM_SubGL)
        End Get
        Set(ByVal Value As Integer)
            iLRM_SubGL = Value
        End Set
    End Property

    Private iLRD_ID As Integer
    Private iLRD_RouteID As Integer
    Private iLRD_PumpID As Integer
    Private sLRD_DelFlag As String
    Private sLRD_Status As String
    Private iLRD_CreatedBy As Integer
    Private dLRD_CreatedOn As Date
    Private iLRD_UpdatedBy As Integer
    Private dLRD_UpdatedOn As Date
    Private iLRD_CompID As Integer
    Private iLRD_YearID As Integer
    Private sLRD_Operation As String
    Private sLRD_IPAddress As String




    Public Property LRD_ID() As Integer
        Get
            Return (iLRD_ID)
        End Get
        Set(ByVal Value As Integer)
            iLRD_ID = Value
        End Set
    End Property
    Public Property LRD_RouteID() As Integer
        Get
            Return (iLRD_RouteID)
        End Get
        Set(ByVal Value As Integer)
            iLRD_RouteID = Value
        End Set
    End Property
    Public Property LRD_PumpID() As Integer
        Get
            Return (iLRD_PumpID)
        End Get
        Set(ByVal Value As Integer)
            iLRD_PumpID = Value
        End Set
    End Property

    Public Property LRD_DelFlag() As String
        Get
            Return (sLRD_DelFlag)
        End Get
        Set(ByVal Value As String)
            sLRD_DelFlag = Value
        End Set
    End Property
    Public Property LRD_Status() As String
        Get
            Return (sLRD_Status)
        End Get
        Set(ByVal Value As String)
            sLRD_Status = Value
        End Set
    End Property
    Public Property LRD_CreatedBy() As Integer
        Get
            Return (iLRD_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iLRD_CreatedBy = Value
        End Set
    End Property
    Public Property LRD_CreatedOn() As DateTime
        Get
            Return (dLRD_CreatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dLRD_CreatedOn = Value
        End Set
    End Property
    Public Property LRD_UpdatedBy() As Integer
        Get
            Return (iLRD_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iLRD_UpdatedBy = Value
        End Set
    End Property
    Public Property LRD_UpdatedOn() As DateTime
        Get
            Return (dLRD_UpdatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dLRD_UpdatedOn = Value
        End Set
    End Property

    Public Property LRD_CompID() As Integer
        Get
            Return (iLRD_CompID)
        End Get
        Set(ByVal Value As Integer)
            iLRD_CompID = Value
        End Set
    End Property
    Public Property LRD_YearID() As Integer
        Get
            Return (iLRD_YearID)
        End Get
        Set(ByVal Value As Integer)
            iLRD_YearID = Value
        End Set
    End Property
    Public Property LRD_Operation() As String
        Get
            Return (sLRD_Operation)
        End Get
        Set(ByVal Value As String)
            sLRD_Operation = Value
        End Set
    End Property
    Public Property LRD_IPAddress() As String
        Get
            Return (sLRD_IPAddress)
        End Get
        Set(ByVal Value As String)
            sLRD_IPAddress = Value
        End Set
    End Property

    Public Function SaveRouteDetails(ByVal sNameSpace As String, ByVal objLRM As clsRouteMaster) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(25) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LRM_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLRM.iLRM_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LRM_VehicleType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLRM.iLRM_VehicleType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LRM_StartPlace", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLRM.iLRM_StartPlace
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LRM_DestPlace", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLRM.iLRM_DestPlace
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LRM_StartDestPlace", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objLRM.sLRM_StartDestPlace
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LRM_DistinKms", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objLRM.dLRM_DistinKms
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LRM_Rate", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objLRM.dLRM_Rate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LRM_DriverAlnceAmt", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objLRM.dLRM_DriverAlnceAmt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LRM_PetrolQty", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objLRM.dLRM_PetrolQty
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LRM_AllottedTime", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objLRM.sLRM_AllottedTime
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LRM_Group", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLRM.iLRM_Group
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LRM_SubGroup", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLRM.iLRM_SubGroup
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LRM_GL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLRM.iLRM_GL
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LRM_SubGL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLRM.iLRM_SubGL
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LRM_Delflag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objLRM.sLRM_Delflag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LRM_Status", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objLRM.sLRM_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LRM_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLRM.iLRM_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LRM_CreatedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objLRM.dLRM_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LRM_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLRM.iLRM_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LRM_UpdatedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objLRM.dLRM_UpdatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LRM_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLRM.iLRM_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LRM_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLRM.iLRM_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LRM_Operation", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objLRM.sLRM_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LRM_IPAddress", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objLRM.sLRM_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spLgst_Route_Master", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SaveRoutePumpkDetails(ByVal sNameSpace As String, ByVal objLRD As clsRouteMaster) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(12) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LRD_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLRD.iLRD_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LRD_RouteID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLRD.iLRD_RouteID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LRD_PumpID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLRD.iLRD_PumpID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LRD_DelFlag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objLRD.sLRD_DelFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LRD_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objLRD.sLRD_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LRD_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLRD.iLRD_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LRD_CreatedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objLRD.dLRD_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LRD_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLRD.iLRD_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LRD_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objLRD.iLRD_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LRD_Operation", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objLRD.sLRD_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@LRD_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objLRD.sLRD_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spLgst_RoutePump_Details", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadRouteMasterlDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iLPMid As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select * from Lgst_Route_Master Where LRM_ID=" & iLPMid & "  And LRM_CompID=" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindRoutePumpDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iRouteID As Integer) As DataTable
        Dim sSql As String
        Dim dr As OleDb.OleDbDataReader
        Dim dt As New DataTable
        Dim dRow As DataRow
        Try
            dt.Columns.Add("ID")
            dt.Columns.Add("PumpName")


            sSql = "Select * from Lgst_RoutePump_Details Where LRD_DelFlag<>'D' And LRD_RouteID=" & iRouteID & " And LRD_CompID=" & iCompID & " Order by LRD_ID"
            dr = objDBL.SQLDataReader(sNameSpace, sSql)

            If dr.HasRows Then
                While dr.Read
                    dRow = dt.NewRow
                    dRow("ID") = dr("LRD_ID")
                    dRow("PumpName") = GetDieselPumpName(sNameSpace, dr("LRD_PumpID"), iCompID)
                    dt.Rows.Add(dRow)
                End While
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadExistingRouteNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select LRM_ID,LRM_StartDestPlace From Lgst_Route_Master Where LRM_CompID=" & iCompID & "" ' and LRM_Delflag='A' "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadRouteDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iStatus As Integer) As DataTable
        Dim dt As New DataTable
        Dim ds As New DataSet
        Dim dc As New DataColumn
        Dim sSql As String = ""
        Dim dr As DataRow
        Dim i As Integer = 0
        Try
            dc = New DataColumn("Id", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("StartPlace", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("DestinationPlace", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("VehicleType", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Status", GetType(String))
            dt.Columns.Add(dc)


            sSql = "Select * from Lgst_Route_Master where LRM_CompID =" & iCompID & " "
            If iStatus = 0 Then
                sSql = sSql & " And LRM_DelFlag ='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & " And LRM_DelFlag='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " And LRM_DelFlag='W'" 'Waiting for approval
            End If
            sSql = sSql & " Order By LRM_ID ASC"

            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dr = dt.NewRow

                    If IsDBNull(ds.Tables(0).Rows(i)("LRM_ID").ToString()) = False Then
                        dr("Id") = ds.Tables(0).Rows(i)("LRM_ID").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("LRM_StartPlace").ToString()) = False Then
                        dr("StartPlace") = GetCityName(sNameSpace, ds.Tables(0).Rows(i)("LRM_StartPlace").ToString())
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("LRM_DestPlace").ToString()) = False Then
                        dr("DestinationPlace") = GetCityName(sNameSpace, ds.Tables(0).Rows(i)("LRM_DestPlace").ToString())
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("LRM_VehicleType").ToString()) = False Then
                        dr("VehicleType") = getVechicleType(sNameSpace, iCompID, ds.Tables(0).Rows(i)("LRM_VehicleType").ToString())
                    End If


                    If (ds.Tables(0).Rows(i)("LRM_DelFlag") = "W") Then
                        dr("Status") = "Waiting for Approval"
                    ElseIf (ds.Tables(0).Rows(i)("LRM_DelFlag") = "A") Then
                        dr("Status") = "Activated"
                    ElseIf (ds.Tables(0).Rows(i)("LRM_DelFlag") = "D") Then
                        dr("Status") = "De-Activated"
                    End If
                    dt.Rows.Add(dr)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateRouteMasterStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasId As Integer, ByVal sDelfalg As String, ByVal iUserID As Integer, ByVal sIPAddress As String, ByVal iYearID As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Update Lgst_Route_Master Set LRM_IPAddress='" & sIPAddress & "',"
            If sDelfalg = "W" Then
                sSql = sSql & " LRM_Status='A',LRM_DelFlag='A',LRM_ApprovedBy= " & iUserID & " , LRM_ApprovedOn=GetDate()"
            ElseIf sDelfalg = "D" Then
                sSql = sSql & " LRM_Status='D',LRM_DelFlag='D',LRM_DeletedBy= " & iUserID & " , LRM_DeletedOn=GetDate()"
            ElseIf sDelfalg = "A" Then
                sSql = sSql & " LRM_Status='A',LRM_DelFlag='A' "
            End If
            sSql = sSql & " Where LRM_CompID=" & iCompID & " And LRM_ID = " & iMasId & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub UpdateRouteMasterStatusWhole(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sSelectedStatus As String, ByVal sDelfalg As String, ByVal iUserID As Integer, ByVal sIPAddress As String, ByVal iMasId As Integer)
        Dim sSql As String = ""
        Dim dtPurchase As New DataTable
        Dim dtSales As New DataTable
        Try
            sSql = "" : sSql = "Select * From Lgst_Route_Master Where LRM_DelFlag='" & sSelectedStatus & "' And LRM_CompID=" & iCompID & "  "
            dtSales = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dtSales.Rows.Count > 0 Then
                For j = 0 To dtSales.Rows.Count - 1
                    sSql = "" : sSql = "Update Lgst_Route_Master Set LRM_IPAddress='" & sIPAddress & "',"
                    If sDelfalg = "W" Then
                        sSql = sSql & " LRM_Status='A',LRM_DelFlag='A',LRM_ApprovedBy= " & iUserID & ",LRM_ApprovedOn=GetDate()"
                    ElseIf sDelfalg = "D" Then
                        sSql = sSql & " LRM_Status='D',LRM_DelFlag='D',LRM_DeletedBy= " & iUserID & ",LRM_DeletedOn=GetDate()"
                    ElseIf sDelfalg = "A" Then
                        sSql = sSql & " LRM_Status='A',LRM_DelFlag='A' "
                    End If
                    sSql = sSql & " Where LRM_CompID=" & iCompID & " And LRM_ID = " & iMasId & ""
                    objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
                Next
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetRoutePumpDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iID As Integer, ByVal iRouteID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * from Lgst_RoutePump_Details Where LRD_DelFlag<>'D' And LRD_ID=" & iID & " And LRD_RouteID=" & iRouteID & " And LRD_CompID=" & iCompID & " Order by LRD_ID"
            GetRoutePumpDetails = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return GetRoutePumpDetails
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub DeleteRoutePumpValue(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iID As Integer, ByVal iRouteID As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Update Lgst_RoutePump_Details Set LRD_DelFlag='D' Where LRD_ID=" & iID & " And LRD_RouteID=" & iRouteID & " And LRD_CompID=" & iCompID & "  "
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetPumpDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iID As Integer, ByVal iRouteID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * from Lgst_RoutePump_Details Where LRD_DelFlag<>'D' And LRD_ID=" & iID & " And LRD_RouteID=" & iRouteID & " And LRD_CompID=" & iCompID & " Order by LRD_ID"
            GetPumpDetails = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return GetPumpDetails
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub DeletePumpValue(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iID As Integer, ByVal iRouteID As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Update Lgst_RoutePump_Details Set LRD_DelFlag='D' Where LRD_ID=" & iID & " And LRD_RouteID=" & iRouteID & " And LRD_CompID=" & iCompID & "  "
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetCityName(ByVal sNameSpace As String, ByVal iMasID As Integer) As String
        Dim sSQL As String = ""
        Dim sCity As String = ""
        Dim dt As New DataTable
        Try

            sSQL = "" : sSQL = "Select Mas_desc  from ACC_General_Master where mas_ID = " & iMasID & " "

            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSQL).Tables(0)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("Mas_desc").ToString()) = False Then
                    sCity = dt.Rows(0)("Mas_desc").ToString()
                Else
                    sCity = ""
                End If
            Else
                sCity = ""
            End If
            Return sCity
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetDieselPumpName(ByVal sNameSpace As String, ByVal iPumpID As Integer, ByVal iCompID As Integer) As String
        Dim sSQL As String = ""
        Dim sPump As String = ""
        Dim dt As New DataTable
        Try

            sSQL = "" : sSQL = "Select LPM_PumpName from Lgst_Pump_Master where LPM_ID = " & iPumpID & " "

            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSQL).Tables(0)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("LPM_PumpName").ToString()) = False Then
                    sPump = dt.Rows(0)("LPM_PumpName").ToString()
                Else
                    sPump = ""
                End If
            Else
                sPump = ""
            End If
            Return sPump
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDieselPump(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select LPM_ID,LPM_PumpName from Lgst_Pump_Master where LPM_CompID =" & iCompID & " and LPM_Delflag='A'  Order By LPM_PumpName"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDestinationCity(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iDestinationCityID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select * from ACC_General_Master where Mas_CompID =" & iCompID & " and Mas_Master in (Select Mas_ID From Acc_Master_Type Where Mas_Type='City' and Mas_DelFlag='X') and Mas_id <> " & iDestinationCityID & " and Mas_Delflag ='A' Order By Mas_Desc"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadVehicleType(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select * from ACC_General_Master where Mas_CompID =" & iCompID & " and Mas_Master in (Select Mas_ID From Acc_Master_Type Where Mas_Type='Vehicle Type' and Mas_DelFlag='X') and Mas_Delflag ='A' Order By Mas_Desc"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function getVechicleType(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iVehicleTypeId As Integer) As String
        Dim sSql As String
        Dim sVehicleType As String = ""
        Dim dt As New DataTable
        Try

            sSql = "Select Mas_desc from ACC_General_Master where  Mas_id =" & iVehicleTypeId & "  "

            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("Mas_desc").ToString()) = False Then
                    sVehicleType = dt.Rows(0)("Mas_desc").ToString()
                Else
                    sVehicleType = ""
                End If
            Else
                sVehicleType = ""
            End If
            Return sVehicleType
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCOAGroup(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sDesc As String) As Integer
        Dim sSql As String = ""
        Dim iCOA As Integer = 0
        Try
            sSql = "" : sSql = "Select GL_ID from Chart_Of_Accounts where GL_Head=0 and GL_Desc = '" & sDesc & "' and gl_CompID=" & iCompID & ""
            iCOA = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return iCOA
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCOASubGroup(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sDesc As String) As Integer
        Dim sSql As String = ""
        Dim iCOA As Integer = 0
        Try
            sSql = "" : sSql = "Select GL_ID from Chart_Of_Accounts where GL_Head=1 and GL_Desc = '" & sDesc & "' and gl_CompID=" & iCompID & ""
            iCOA = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return iCOA
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCOAGL(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sDesc As String) As Integer
        Dim sSql As String = ""
        Dim iCOA As Integer = 0
        Try
            sSql = "" : sSql = "Select GL_ID from Chart_Of_Accounts where GL_Head=2 and GL_Desc = '" & sDesc & "' and gl_CompID=" & iCompID & ""
            iCOA = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return iCOA
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckRouteNameDuplicate(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sLRM_StartDestPlace As String) As Boolean
        Dim sSql As String
        Try
            sSql = "Select * From lgst_route_master Where LRM_StartDestPlace='" & sLRM_StartDestPlace & "' And LRM_DelFlag<>'D' And LRM_CompID=" & iCompID & "  "
            CheckRouteNameDuplicate = objDBL.SQLCheckForRecord(sNameSpace, sSql)
            Return CheckRouteNameDuplicate
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
