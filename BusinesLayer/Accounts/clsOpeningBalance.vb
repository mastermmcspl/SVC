Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class clsOpeningBalance
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral

    Private Opn_Id As Integer
    Private Opn_Date As DateTime
    Private Opn_AccHead As Integer
    Private Opn_GLCode As String
    Private Opn_DebitAmt As Double
    Private Opn_CreditAmount As Double
    Private Opn_YearId As Integer
    Private Opn_Status As String
    Private Opn_CompId As Integer
    Private Opn_GlId As Integer
    Private Opn_IPAddress As String
    Private Opn_Manual As Integer

    Private Opn_ZoneID As Integer
    Private Opn_RegionID As Integer
    Private Opn_AreaID As Integer
    Private Opn_BranchID As Integer

    Public Property iOpn_Manual() As Integer
        Get
            Return (Opn_Manual)
        End Get
        Set(ByVal Value As Integer)
            Opn_Manual = Value
        End Set
    End Property
    Public Property sOpn_IPAddress() As String
        Get
            Return (Opn_IPAddress)
        End Get
        Set(ByVal Value As String)
            Opn_IPAddress = Value
        End Set
    End Property
    Public Property iOpn_GlId() As Integer
        Get
            Return (Opn_GlId)
        End Get
        Set(ByVal Value As Integer)
            Opn_GlId = Value
        End Set
    End Property
    Public Property iOpn_CompId() As Integer
        Get
            Return (Opn_CompId)
        End Get
        Set(ByVal Value As Integer)
            Opn_CompId = Value
        End Set
    End Property
    Public Property sOpn_Status() As String
        Get
            Return (Opn_Status)
        End Get
        Set(ByVal Value As String)
            Opn_Status = Value
        End Set
    End Property

    Public Property iOpn_YearId() As Integer
        Get
            Return (Opn_YearId)
        End Get
        Set(ByVal Value As Integer)
            Opn_YearId = Value
        End Set
    End Property
    Public Property dOpn_CreditAmount() As Double
        Get
            Return (Opn_CreditAmount)
        End Get
        Set(ByVal Value As Double)
            Opn_CreditAmount = Value
        End Set
    End Property
    Public Property dOpn_DebitAmt() As Double
        Get
            Return (Opn_DebitAmt)
        End Get
        Set(ByVal Value As Double)
            Opn_DebitAmt = Value
        End Set
    End Property
    Public Property sOpn_GLCode() As String
        Get
            Return (Opn_GLCode)
        End Get
        Set(ByVal Value As String)
            Opn_GLCode = Value
        End Set
    End Property
    Public Property iOpn_AccHead() As Integer
        Get
            Return (Opn_AccHead)
        End Get
        Set(ByVal Value As Integer)
            Opn_AccHead = Value
        End Set
    End Property

    Public Property dOpn_Date() As DateTime
        Get
            Return (Opn_Date)
        End Get
        Set(ByVal Value As DateTime)
            Opn_Date = Value
        End Set
    End Property
    Public Property iOpn_Id() As Integer
        Get
            Return (Opn_Id)
        End Get
        Set(ByVal Value As Integer)
            Opn_Id = Value
        End Set
    End Property

    Public Property iOpn_ZoneID() As Integer
        Get
            Return (Opn_ZoneID)
        End Get
        Set(ByVal Value As Integer)
            Opn_ZoneID = Value
        End Set
    End Property
    Public Property iOpn_RegionID() As Integer
        Get
            Return (Opn_RegionID)
        End Get
        Set(ByVal Value As Integer)
            Opn_RegionID = Value
        End Set
    End Property
    Public Property iOpn_AreaID() As Integer
        Get
            Return (Opn_AreaID)
        End Get
        Set(ByVal Value As Integer)
            Opn_AreaID = Value
        End Set
    End Property
    Public Property iOpn_BranchID() As Integer
        Get
            Return (Opn_BranchID)
        End Get
        Set(ByVal Value As Integer)
            Opn_BranchID = Value
        End Set
    End Property


    'Kishore

    Private SubOpn_Id As Integer
    Private SubCreatedOpn_Date As Date
    Private SubOpn_AccHead As Integer
    Private SubOpn_GLCode As String
    Private SubOpn_DebitAmt As Double
    Private SubOpn_CreditAmount As Double
    Private SubOpn_YearId As Integer
    Private SubOpn_Status As String
    Private SubOpn_CompId As Integer
    Private SubOpn_GlId As Integer
    Private SubOpn_IPAddress As String
    Private SubOpn_ReferenceNo As String
    Private SubPenidingAmount As Double
    Private SubDueOn As Date
    Private SubOpn_OverDueDays As Integer
    Private SubBillDate As Date

    Public Property sSubOpn_IPAddress() As String
        Get
            Return (SubOpn_IPAddress)
        End Get
        Set(ByVal Value As String)
            SubOpn_IPAddress = Value
        End Set
    End Property
    Public Property iSubOpn_GlId() As Integer
        Get
            Return (SubOpn_GlId)
        End Get
        Set(ByVal Value As Integer)
            SubOpn_GlId = Value
        End Set
    End Property
    Public Property iSubOpn_CompId() As Integer
        Get
            Return (SubOpn_CompId)
        End Get
        Set(ByVal Value As Integer)
            SubOpn_CompId = Value
        End Set
    End Property
    Public Property sSubOpn_Status() As String
        Get
            Return (SubOpn_Status)
        End Get
        Set(ByVal Value As String)
            SubOpn_Status = Value
        End Set
    End Property

    Public Property iSubOpn_YearId() As Integer
        Get
            Return (SubOpn_YearId)
        End Get
        Set(ByVal Value As Integer)
            SubOpn_YearId = Value
        End Set
    End Property
    Public Property dSubOpn_CreditAmount() As Double
        Get
            Return (SubOpn_CreditAmount)
        End Get
        Set(ByVal Value As Double)
            SubOpn_CreditAmount = Value
        End Set
    End Property
    Public Property dSubOpn_DebitAmt() As Double
        Get
            Return (SubOpn_DebitAmt)
        End Get
        Set(ByVal Value As Double)
            SubOpn_DebitAmt = Value
        End Set
    End Property
    Public Property sSubOpn_GLCode() As String
        Get
            Return (SubOpn_GLCode)
        End Get
        Set(ByVal Value As String)
            SubOpn_GLCode = Value
        End Set
    End Property
    Public Property iSubOpn_AccHead() As Integer
        Get
            Return (SubOpn_AccHead)
        End Get
        Set(ByVal Value As Integer)
            SubOpn_AccHead = Value
        End Set
    End Property
    Public Property iSubOpn_OverDueDays() As Integer
        Get
            Return (SubOpn_OverDueDays)
        End Get
        Set(ByVal Value As Integer)
            SubOpn_OverDueDays = Value
        End Set
    End Property
    Public Property dSubDueOn() As Date
        Get
            Return (SubDueOn)
        End Get
        Set(ByVal Value As Date)
            SubDueOn = Value
        End Set
    End Property
    Public Property dSubPenidingAmount() As Double
        Get
            Return (SubPenidingAmount)
        End Get
        Set(ByVal Value As Double)
            SubPenidingAmount = Value
        End Set
    End Property
    Public Property sSubOpn_ReferenceNo() As String
        Get
            Return (SubOpn_ReferenceNo)
        End Get
        Set(ByVal Value As String)
            SubOpn_ReferenceNo = Value
        End Set
    End Property

    Public Property dSubCreatedOpn_Date() As Date
        Get
            Return (SubCreatedOpn_Date)
        End Get
        Set(ByVal Value As Date)
            SubCreatedOpn_Date = Value
        End Set
    End Property
    Public Property iSubOpn_Id() As Integer
        Get
            Return (SubOpn_Id)
        End Get
        Set(ByVal Value As Integer)
            SubOpn_Id = Value
        End Set
    End Property

    Public Property dSubBillDate() As Date
        Get
            Return (SubBillDate)
        End Get
        Set(ByVal Value As Date)
            SubBillDate = Value
        End Set
    End Property

    Public Function LoadGroup(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iHead As Integer) As DataSet
        Dim sSql As String = ""
        Dim ds As New DataSet
        Try
            sSql = "" : sSql = "Select gl_id, (gl_glCode + ' - ' + gl_desc) as Description from Chart_Of_Accounts where gl_head = 0 and "
            sSql = sSql & "gl_AccHead =" & iHead & "  and gl_CompId =" & iCompID & " and gl_Status ='A' order by gl_id"
            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            Return ds
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadSubGroup(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iGroup As Integer) As DataSet
        Dim sSql As String = ""
        Dim ds As New DataSet
        Try
            sSql = "" : sSql = "Select gl_id, (gl_glCode + ' - ' + gl_desc) as Description from Chart_Of_Accounts where gl_head = 1 and "
            sSql = sSql & "gl_Parent =" & iGroup & " And gl_CompId =" & iCompID & " and gl_Status ='A' order by gl_id"
            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            Return ds
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadGrdGL(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iHead As Integer, ByVal iGlID As Integer, ByVal iSubGL As Integer) As DataTable
        Dim sSql As String = "", asql As String = ""
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim dtGL As New DataTable
        Dim i As Integer = 0
        Dim dBalance As Double = 0
        Try
            dt.Columns.Add("Head")
            dt.Columns.Add("GLID")
            dt.Columns.Add("AccHead")
            dt.Columns.Add("SLNo")
            dt.Columns.Add("GLCode")
            dt.Columns.Add("GLDescription")
            dt.Columns.Add("Debit")
            dt.Columns.Add("SubGLDebit")
            dt.Columns.Add("Credit")
            dt.Columns.Add("SubGLCredit")
            dt.Columns.Add("Balance")
            dt.Columns.Add("StartDate")

            sSql = "" : sSql = "Select distinct A.gl_Id,A.gl_AccHead,A.gl_glcode,A.gl_Desc,A.gl_Parent,B.Opn_Date,B.Opn_DebitAmt,B.Opn_CreditAmount,A.gl_Head "
            sSql = sSql & "from chart_of_Accounts A Left join Acc_opening_balance B On B.Opn_glid = A.Gl_ID "
            sSql = sSql & " Where A.gl_Head<>0 And A.gl_Head<>1 And A.gl_compid=" & iCompID & " And A.gl_delflag='C' and A.gl_Status ='A' and (A.gl_head=2 or A.gl_head=3) order by A.gl_glcode"
            dtGL = objDBL.SQLExecuteDataTable(sNameSpace, sSql)

            If (iHead > 0) And (iGlID = 0) And (iSubGL = 0) Then
                Dim DVGLCODE As New DataView(dtGL)
                DVGLCODE.RowFilter = "gl_AccHead=" & iHead
                dtGL = DVGLCODE.ToTable

            ElseIf (iHead > 0) And (iGlID > 0) And (iSubGL = 0) Then
                Dim DVGLCODE As New DataView(dtGL)
                DVGLCODE.RowFilter = "gl_Parent=" & iGlID
                dtGL = DVGLCODE.ToTable
            ElseIf (iHead > 0) And (iGlID > 0) And (iSubGL > 0) Then
                Dim DVGLCODE As New DataView(dtGL)
                DVGLCODE.RowFilter = "gl_Parent=" & iSubGL
                dtGL = DVGLCODE.ToTable
            End If

            If dtGL.Rows.Count > 0 Then
                For i = 0 To dtGL.Rows.Count - 1
                    dRow = dt.NewRow()

                    dRow("SLNo") = i + 1

                    If IsDBNull(dtGL.Rows(i)("gl_head").ToString()) = False Then
                        dRow("Head") = dtGL.Rows(i)("gl_head").ToString()
                    End If

                    If IsDBNull(dtGL.Rows(i)("gl_Id").ToString()) = False Then
                        dRow("GLID") = dtGL.Rows(i)("gl_Id").ToString()
                    End If

                    If IsDBNull(dtGL.Rows(i)("gl_AccHead").ToString()) = False Then
                        dRow("AccHead") = dtGL.Rows(i)("gl_AccHead").ToString()
                    End If

                    If IsDBNull(dtGL.Rows(i)("gl_glcode").ToString()) = False Then
                        dRow("GLCode") = dtGL.Rows(i)("gl_glcode").ToString()
                    End If

                    If IsDBNull(dtGL.Rows(i)("gl_Desc").ToString()) = False Then
                        dRow("GLDescription") = dtGL.Rows(i)("gl_Desc").ToString()
                    End If

                    If dtGL.Rows(i)("Opn_Date").ToString() <> "" Then
                        dRow("StartDate") = objGen.FormatDtForRDBMS(dtGL.Rows(i)("Opn_Date"), "D")
                    End If

                    If dtGL.Rows(i)("gl_head").ToString() = 2 Then  'GL
                        If (IsDBNull(dtGL.Rows(i)("Opn_DebitAmt").ToString()) = False) And (dtGL.Rows(i)("Opn_DebitAmt").ToString() <> "") Then
                            If Convert.ToDouble(dtGL.Rows(i)("Opn_DebitAmt").ToString()) = 0.00 Then
                                dRow("Debit") = "0.00"
                            Else
                                dRow("Debit") = Convert.ToDecimal(dtGL.Rows(i)("Opn_DebitAmt").ToString()).ToString("#,##0.00")
                            End If
                        Else
                            dRow("Debit") = "0.00"
                        End If

                        If (IsDBNull(dtGL.Rows(i)("Opn_CreditAmount").ToString()) = False) And (dtGL.Rows(i)("Opn_CreditAmount").ToString() <> "") Then
                            If Convert.ToDouble(dtGL.Rows(i)("Opn_CreditAmount").ToString()) = 0.00 Then
                                dRow("Credit") = "0.00"
                            Else
                                dRow("Credit") = Convert.ToDecimal(dtGL.Rows(i)("Opn_CreditAmount").ToString()).ToString("#,##0.00")
                            End If
                        Else
                            dRow("Credit") = "0.00"
                        End If

                        dBalance = Convert.ToDecimal(dRow("Debit") - dRow("Credit")).ToString("#,##0.00")
                    ElseIf dtGL.Rows(i)("gl_head").ToString() = 3 Then  'SubGL
                        If (IsDBNull(dtGL.Rows(i)("Opn_DebitAmt").ToString()) = False) And (dtGL.Rows(i)("Opn_DebitAmt").ToString() <> "") Then
                            If Convert.ToDouble(dtGL.Rows(i)("Opn_DebitAmt").ToString()) = 0.00 Then
                                dRow("SubGLDebit") = "0.00"
                            Else
                                dRow("SubGLDebit") = Convert.ToDecimal(dtGL.Rows(i)("Opn_DebitAmt").ToString()).ToString("#,##0.00")
                            End If
                        Else
                            dRow("SubGLDebit") = "0.00"
                        End If

                        If (IsDBNull(dtGL.Rows(i)("Opn_CreditAmount").ToString()) = False) And (dtGL.Rows(i)("Opn_CreditAmount").ToString() <> "") Then
                            If Convert.ToDouble(dtGL.Rows(i)("Opn_CreditAmount").ToString()) = 0.00 Then
                                dRow("SubGLCredit") = "0.00"
                            Else
                                dRow("SubGLCredit") = Convert.ToDecimal(dtGL.Rows(i)("Opn_CreditAmount").ToString()).ToString("#,##0.00")
                            End If
                        Else
                            dRow("SubGLCredit") = "0.00"
                        End If

                        dBalance = "0.00"
                    End If

                    'dBalance = Convert.ToDecimal(dRow("Debit") - dRow("Credit")).ToString("#,##0.00")
                    If dBalance < 0 Then
                        dRow("Balance") = dBalance & " Cr"
                    Else
                        dRow("Balance") = dBalance & " Dr"
                    End If

                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SaveOpeningBalance(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iUserID As Integer, ByVal objOP As clsOpeningBalance)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(25) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_Id", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOP.iOpn_Id
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_SerialNo", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 1
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_Date", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objOP.dOpn_Date
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_AccHead", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOP.iOpn_AccHead
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_GLCode", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objOP.sOpn_GLCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_DebitAmt", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objOP.dOpn_DebitAmt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_CreditAmount", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objOP.dOpn_CreditAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_YearId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOP.iOpn_YearId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iUserID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_CreatedOn", OleDb.OleDbType.Date, 500)
            ObjParam(iParamCount).Value = Date.Today
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_ApprovedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iUserID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_ApprovedOn", OleDb.OleDbType.Date, 500)
            ObjParam(iParamCount).Value = Date.Today
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = "A"
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_CompId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOP.iOpn_CompId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_GlId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOP.iOpn_GlId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_Operation", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = "C"
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objOP.Opn_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_CustType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_IndType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_Manual", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOP.Opn_Manual
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_ZoneID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objOP.iOpn_ZoneID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_RegionID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objOP.iOpn_RegionID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_AreaID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objOP.iOpn_AreaID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_BranchID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objOP.iOpn_BranchID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spACC_Opening_Balance", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveSubLedgerOpeningBalance(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iUserID As Integer, ByVal objOP As clsOpeningBalance)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(22) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_Id", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOP.iSubOpn_Id
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_SerialNo", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 1
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_AccHead", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_GLCode", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objOP.sSubOpn_GLCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_DebitAmt", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objOP.dSubOpn_DebitAmt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_CreditAmount", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objOP.dSubOpn_CreditAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_YearId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOP.iSubOpn_YearId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iUserID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_CreatedOn", OleDb.OleDbType.Date, 500)
            ObjParam(iParamCount).Value = Date.Today
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_ApprovedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iUserID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_ApprovedOn", OleDb.OleDbType.Date, 500)
            ObjParam(iParamCount).Value = Date.Today
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = "A"
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_CompId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOP.iSubOpn_CompId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_GlId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOP.iSubOpn_GlId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_Operation", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = "C"
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objOP.sSubOpn_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_ReferenceNo", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objOP.sSubOpn_ReferenceNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_BilDate", OleDb.OleDbType.Date, 500)
            ObjParam(iParamCount).Value = objOP.dSubBillDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_PendingAmnt", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objOP.dSubPenidingAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_DueOnDate", OleDb.OleDbType.Date, 500)
            ObjParam(iParamCount).Value = Date.Today
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Opn_OverDueDays", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOP.iSubOpn_OverDueDays
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"
            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spACC_SubLedgerOpening_Balance", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetManual(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select Distinct(Opn_Manual) From Acc_Opening_Balance Where Opn_CompID=" & iCompID & " And Opn_YearID=" & iYearID & " "
            GetManual = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return GetManual
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindBreakUPDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iGLID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dtTab As New DataTable
        Dim dr As DataRow
        Try
            dtTab.Columns.Add("GLID")
            dtTab.Columns.Add("ID")
            dtTab.Columns.Add("BillNo")
            dtTab.Columns.Add("BillDate")
            dtTab.Columns.Add("Debit")
            dtTab.Columns.Add("Credit")

            sSql = "Select * from Op_BreakUp Where opb_GLID=" & iGLID & " And opb_CompID=" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow

                dr("GLID") = dt.Rows(i)("opb_GLID")
                dr("ID") = dt.Rows(i)("opb_ID")
                If (dt.Rows(i)("opb_BillNo").ToString() = "") Then
                    dr("BillNo") = 0
                Else
                    dr("BillNo") = dt.Rows(i)("opb_BillNo")
                End If
                If (objGen.FormatDtForRDBMS(dt.Rows(i)("opb_Billdate"), "D").ToString() = "01/01/1900") Or (objGen.FormatDtForRDBMS(dt.Rows(i)("opb_Billdate"), "D").ToString() = "01-01-1900") Then
                    dr("BillDate") = ""
                Else
                    dr("BillDate") = objGen.FormatDtForRDBMS(dt.Rows(i)("opb_Billdate"), "D")
                End If

                If (dt.Rows(i)("opb_Debit").ToString() = "") Then
                    dr("Debit") = 0
                Else
                    dr("Debit") = dt.Rows(i)("opb_Debit")
                End If

                If (dt.Rows(i)("opb_Credit").ToString() = "") Then
                    dr("Credit") = 0
                Else
                    dr("Credit") = dt.Rows(i)("opb_Credit")
                End If

                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAccZone(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Org_Node,Org_Name From Sad_Org_Structure Where Org_Parent in(Select Org_Node From Sad_Org_Structure Where Org_Parent=0 and Org_CompID=" & iCompID & " )"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAccRgn(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAccZone As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If iAccZone > 0 Then
                sSql = "" : sSql = "Select Org_Node,Org_Name From Sad_Org_Structure Where Org_Parent=" & iAccZone & " And Org_CompID=" & iCompID & " "
            Else
                sSql = "" : sSql = "Select Org_Node,Org_Name From Sad_Org_Structure Where Org_LevelCode=2 And Org_CompID=" & iCompID & " "
            End If
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAccArea(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAccRgn As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If iAccRgn > 0 Then
                sSql = "" : sSql = "Select Org_Node,Org_Name From Sad_Org_Structure Where Org_Parent=" & iAccRgn & " And Org_CompID=" & iCompID & " "
            Else
                sSql = "" : sSql = "Select Org_Node,Org_Name From Sad_Org_Structure Where Org_LevelCode=3 And Org_CompID=" & iCompID & " "
            End If
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAccBrnch(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAccarea As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If iAccarea > 0 Then
                sSql = "" : sSql = "Select Org_Node,Org_Name From Sad_Org_Structure Where Org_Parent=" & iAccarea & " And Org_CompID=" & iCompID & " "
            Else
                sSql = "" : sSql = "Select Org_Node,Org_Name From Sad_Org_Structure Where Org_LevelCode=4 And Org_CompID=" & iCompID & " "
            End If
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getOrgParent(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iNodeID As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Select Org_Parent From Sad_Org_Structure Where Org_Node=" & iNodeID & " And Org_CompID=" & iCompID & " "
            getOrgParent = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return getOrgParent
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDefaultBranch(ByVal sNameSpace As String, ByVal iCompID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select Org_Node From Sad_Org_Structure Where Org_LevelCode=4 And Org_Default=1 And Org_CompID=" & iCompID & " "
            GetDefaultBranch = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return GetDefaultBranch
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetTotalSubGLDebit(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iGLID As Integer) As Double
        Dim sSql As String = ""
        Dim ssql1 As String = ""
        Dim ssql2 As String = ""
        Dim count, parentid As Integer
        Dim bCheck As Boolean : Dim bOpnCheck As Boolean
        Try
            ssql1 = "Select gl_id From Chart_Of_Accounts Where gl_head=2 and GL_id = " & iGLID & " And GL_CompID=" & iCompID & " "
            parentid = objDBL.SQLExecuteScalar(sNameSpace, ssql1)
            ssql2 = "Select count(*) From Chart_Of_Accounts Where gl_head=3 and GL_parent= " & parentid & " and gl_status='A' And GL_CompID=" & iCompID & " "
            count = objDBL.SQLExecuteScalar(sNameSpace, ssql2)
            If (count > 0) Then
                bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select Gl_ID From Chart_Of_Accounts Where GL_Head=3 And GL_parent=" & iGLID & " And GL_CompID=" & iCompID & " ")
                If bCheck = True Then
                    bOpnCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select * From Acc_Opening_Balance Where Opn_GLID in(Select Gl_ID From Chart_Of_Accounts Where GL_Head=3 And GL_parent=" & iGLID & ") And Opn_YearID=" & iYearID & " And Opn_CompID=" & iCompID & " ")
                    If bOpnCheck = True Then
                        sSql = "Select Sum(Opn_DebitAmt) From Acc_Opening_Balance Where Opn_GLID in(Select Gl_ID From Chart_Of_Accounts Where GL_Head=3 And GL_parent=" & iGLID & ") And Opn_YearID=" & iYearID & " And Opn_CompID=" & iCompID & " "
                        GetTotalSubGLDebit = objDBL.SQLGetDescription(sNameSpace, sSql)
                    Else
                        GetTotalSubGLDebit = 0.0
                    End If
                Else
                    GetTotalSubGLDebit = 0.0
                End If
            Else
                bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select Gl_ID From Chart_Of_Accounts Where GL_Head=2 And GL_id=" & iGLID & " And GL_CompID=" & iCompID & " ") ' add else statement to get gl debit total
                If bCheck = True Then
                    bOpnCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select * From Acc_Opening_Balance Where Opn_GLID in(Select Gl_ID From Chart_Of_Accounts Where GL_Head=2 And GL_ID=" & iGLID & ") And Opn_YearID=" & iYearID & " And Opn_CompID=" & iCompID & " ")
                    If bOpnCheck = True Then
                        sSql = "Select Sum(Opn_DebitAmt) From Acc_Opening_Balance Where Opn_GLID in(Select Gl_ID From Chart_Of_Accounts Where GL_Head=2 And GL_ID=" & iGLID & ") And Opn_YearID=" & iYearID & " And Opn_CompID=" & iCompID & " "
                        GetTotalSubGLDebit = objDBL.SQLGetDescription(sNameSpace, sSql)
                    Else
                        GetTotalSubGLDebit = 0.0
                    End If
                Else
                    GetTotalSubGLDebit = 0.0
                End If
                '
            End If
            Return GetTotalSubGLDebit
        Catch ex As Exception
            Throw
        End Try

        'Commented this code because of not getting gl debit Amount total, inserted else statement to get the sum of gl(if their is no subgls) in the code

        'Dim sSql As String = ""
        'Dim bCheck As Boolean : Dim bOpnCheck As Boolean
        'Try
        '    bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select Gl_ID From Chart_Of_Accounts Where GL_Head=3 And GL_parent=" & iGLID & " ")
        '    If bCheck = True Then
        '        bOpnCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select * From Acc_Opening_Balance Where Opn_GLID in(Select Gl_ID From Chart_Of_Accounts Where GL_Head=3 And GL_parent=" & iGLID & ") And Opn_YearID=" & iYearID & " And Opn_CompID=" & iCompID & " ")
        '        If bOpnCheck = True Then
        '            sSql = "Select Sum(Opn_DebitAmt) From Acc_Opening_Balance Where Opn_GLID in(Select Gl_ID From Chart_Of_Accounts Where GL_Head=3 And GL_parent=" & iGLID & ") And Opn_YearID=" & iYearID & " And Opn_CompID=" & iCompID & " "
        '            GetTotalSubGLDebit = objDBL.SQLGetDescription(sNameSpace, sSql)
        '        Else
        '            GetTotalSubGLDebit = 0.0
        '        End If
        '    Else
        '        'GetTotalSubGLDebit = 0.0
        '    End If
        '    Return GetTotalSubGLDebit
        'Catch ex As Exception
        '    Throw
        'End Try
    End Function
    Public Function GetTotalSubGLCredit(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iGLID As Integer) As Double

        Dim sSql As String = ""
        Dim ssql1 As String = ""
        Dim ssql2 As String = ""
        Dim count, parentid As Integer
        Dim bCheck As Boolean : Dim bOpnCheck As Boolean
        Try
            ssql1 = "Select gl_id From Chart_Of_Accounts Where gl_head=2 and GL_id = " & iGLID & " And GL_CompID=" & iCompID & " "
            parentid = objDBL.SQLExecuteScalar(sNameSpace, ssql1)
            ssql2 = "Select count(*) From Chart_Of_Accounts Where gl_head=3 and GL_parent= " & parentid & " and gl_status='A' And GL_CompID=" & iCompID & " "
            count = objDBL.SQLExecuteScalar(sNameSpace, ssql2)
            If (count > 0) Then
                bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select Gl_ID From Chart_Of_Accounts Where GL_Head=3 And GL_parent=" & iGLID & " And GL_CompID=" & iCompID & " ")
                If bCheck = True Then
                    bOpnCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select * From Acc_Opening_Balance Where Opn_GLID in(Select Gl_ID From Chart_Of_Accounts Where GL_Head=3 And GL_parent=" & iGLID & ") And Opn_YearID=" & iYearID & " And Opn_CompID=" & iCompID & " ")
                    If bOpnCheck = True Then
                        sSql = "Select Sum(Opn_CreditAmount) From Acc_Opening_Balance Where Opn_GLID in(Select Gl_ID From Chart_Of_Accounts Where GL_Head=3 And GL_parent=" & iGLID & ") And Opn_YearID=" & iYearID & " And Opn_CompID=" & iCompID & " "
                        GetTotalSubGLCredit = objDBL.SQLGetDescription(sNameSpace, sSql)
                    Else
                        GetTotalSubGLCredit = 0.0
                    End If
                Else
                    GetTotalSubGLCredit = 0.0
                End If
            Else
                bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select Gl_ID From Chart_Of_Accounts Where GL_Head=2 And GL_id=" & iGLID & " And GL_CompID=" & iCompID & " ") ' add else statement to get gl debit total
                If bCheck = True Then
                    bOpnCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select * From Acc_Opening_Balance Where Opn_GLID in(Select Gl_ID From Chart_Of_Accounts Where GL_Head=2 And GL_ID=" & iGLID & ") And Opn_YearID=" & iYearID & " And Opn_CompID=" & iCompID & " ")
                    If bOpnCheck = True Then
                        sSql = "Select Sum(Opn_CreditAmount) From Acc_Opening_Balance Where Opn_GLID in(Select Gl_ID From Chart_Of_Accounts Where GL_Head=2 And GL_ID=" & iGLID & ") And Opn_YearID=" & iYearID & " And Opn_CompID=" & iCompID & " "
                        GetTotalSubGLCredit = objDBL.SQLGetDescription(sNameSpace, sSql)
                    Else
                        GetTotalSubGLCredit = 0.0
                    End If
                Else
                    GetTotalSubGLCredit = 0.0
                End If
                '
            End If
            Return GetTotalSubGLCredit
        Catch ex As Exception
            Throw
        End Try
        'Commented this code because of not getting gl credit amount total, inserted else statement to get the sum of gl(if their is no subgls) in the code

        'Dim sSql As String = ""
        'Dim bCheck As Boolean : Dim bOpnCheck As Boolean
        'Try
        '    bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select Gl_ID From Chart_Of_Accounts Where GL_Head=3 And GL_parent=" & iGLID & " ")
        '    If bCheck = True Then
        '        bOpnCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select * From Acc_Opening_Balance Where Opn_GLID in(Select Gl_ID From Chart_Of_Accounts Where GL_Head=3 And GL_parent=" & iGLID & ") And Opn_YearID=" & iYearID & " And Opn_CompID=" & iCompID & " ")
        '        If bOpnCheck = True Then
        '            sSql = "Select Sum(Opn_CreditAmount) From Acc_Opening_Balance Where Opn_GLID in(Select Gl_ID From Chart_Of_Accounts Where GL_Head=3 And GL_parent=" & iGLID & ") And Opn_YearID=" & iYearID & " And Opn_CompID=" & iCompID & " "
        '            GetTotalSubGLCredit = objDBL.SQLGetDescription(sNameSpace, sSql)
        '        Else
        '            GetTotalSubGLCredit = 0.0
        '        End If
        '    Else
        '        GetTotalSubGLCredit = 0.0
        '    End If
        '    Return GetTotalSubGLCredit
        'Catch ex As Exception
        '    Throw
        'End Try
    End Function
End Class
