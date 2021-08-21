Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class clsFixedAssetJournalEntry
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral

    Private iAFJ_ID As Integer
    Private sAFJ_TransactionNo As String
    Private iAFJ_Party As Integer
    Private iAFJ_Location As Integer
    Private iAFJ_Block As Integer
    Private dAFJ_Amount As Double
    Private sAFJ_Narration As String
    Private sAFJ_Status As String
    Private iAFJ_CreatedBy As Integer
    Private dAFJ_CreatedOn As DateTime
    Private iAFJ_ApprovedBy As Integer
    Private dAFJ_ApprovedOn As DateTime
    Private iAFJ_DeletedBy As Integer
    Private dAFJ_DeletedOn As DateTime
    Private iAFJ_RecalledBy As Integer
    Private dAFJ_RecalledOn As DateTime
    Private iAFJ_YearID As Integer
    Private iAFJ_CompID As Integer
    Private sAFJ_Operation As String
    Private sAFJ_IPAddress As String

    Private iAFJD_Id As Integer
    Private iAFJD_MasterID As Integer
    Private iAFJD_Head As Integer
    Private iAFJD_GL As Integer
    Private iAFJD_SubGL As Integer
    Private dAFJD_Debit As Double
    Private dAFJD_Credit As Double
    Private iAFJD_YearID As Integer
    Private iAFJD_CompID As Integer
    Private sAFJD_Status As String
    Private sAFJD_Operation As String
    Private sAFJD_IPAddress As String

    Public Property AFJ_ID() As Integer
        Get
            Return (iAFJ_ID)
        End Get
        Set(ByVal Value As Integer)
            iAFJ_ID = Value
        End Set
    End Property
    Public Property AFJ_TransactionNo() As String
        Get
            Return (sAFJ_TransactionNo)
        End Get
        Set(ByVal Value As String)
            sAFJ_TransactionNo = Value
        End Set
    End Property
    Public Property AFJ_Party() As Integer
        Get
            Return (iAFJ_Party)
        End Get
        Set(ByVal Value As Integer)
            iAFJ_Party = Value
        End Set
    End Property
    Public Property AFJ_Location() As Integer
        Get
            Return (iAFJ_Location)
        End Get
        Set(ByVal Value As Integer)
            iAFJ_Location = Value
        End Set
    End Property
    Public Property AFJ_Block() As Integer
        Get
            Return (iAFJ_Block)
        End Get
        Set(ByVal Value As Integer)
            iAFJ_Block = Value
        End Set
    End Property
    Public Property AFJ_Amount() As Double
        Get
            Return (dAFJ_Amount)
        End Get
        Set(ByVal Value As Double)
            dAFJ_Amount = Value
        End Set
    End Property
    Public Property AFJ_Narration() As String
        Get
            Return (sAFJ_Narration)
        End Get
        Set(ByVal Value As String)
            sAFJ_Narration = Value
        End Set
    End Property
    Public Property AFJ_Status() As String
        Get
            Return (sAFJ_Status)
        End Get
        Set(ByVal Value As String)
            sAFJ_Status = Value
        End Set
    End Property
    Public Property AFJ_CreatedBy() As Integer
        Get
            Return (iAFJ_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iAFJ_CreatedBy = Value
        End Set
    End Property
    Public Property AFJ_CreatedOn() As DateTime
        Get
            Return (dAFJ_CreatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dAFJ_CreatedOn = Value
        End Set
    End Property
    Public Property AFJ_ApprovedBy() As Integer
        Get
            Return (iAFJ_ApprovedBy)
        End Get
        Set(ByVal Value As Integer)
            iAFJ_ApprovedBy = Value
        End Set
    End Property
    Public Property AFJ_ApprovedOn() As DateTime
        Get
            Return (dAFJ_ApprovedOn)
        End Get
        Set(ByVal Value As DateTime)
            dAFJ_ApprovedOn = Value
        End Set
    End Property
    Public Property AFJ_DeletedBy() As Integer
        Get
            Return (iAFJ_DeletedBy)
        End Get
        Set(ByVal Value As Integer)
            iAFJ_DeletedBy = Value
        End Set
    End Property
    Public Property AFJ_DeletedOn() As DateTime
        Get
            Return (dAFJ_DeletedOn)
        End Get
        Set(ByVal Value As DateTime)
            dAFJ_DeletedOn = Value
        End Set
    End Property
    Public Property AFJ_RecalledBy() As Integer
        Get
            Return (iAFJ_RecalledBy)
        End Get
        Set(ByVal Value As Integer)
            iAFJ_RecalledBy = Value
        End Set
    End Property
    Public Property AFJ_RecalledOn() As DateTime
        Get
            Return (dAFJ_RecalledOn)
        End Get
        Set(ByVal Value As DateTime)
            dAFJ_RecalledOn = Value
        End Set
    End Property
    Public Property AFJ_YearID() As Integer
        Get
            Return (iAFJ_YearID)
        End Get
        Set(ByVal Value As Integer)
            iAFJ_YearID = Value
        End Set
    End Property
    Public Property AFJ_CompID() As Integer
        Get
            Return (iAFJ_CompID)
        End Get
        Set(ByVal Value As Integer)
            iAFJ_CompID = Value
        End Set
    End Property
    Public Property AFJ_Operation() As String
        Get
            Return (sAFJ_Operation)
        End Get
        Set(ByVal Value As String)
            sAFJ_Operation = Value
        End Set
    End Property
    Public Property AFJ_IPAddress() As String
        Get
            Return (sAFJ_IPAddress)
        End Get
        Set(ByVal Value As String)
            sAFJ_IPAddress = Value
        End Set
    End Property


    Public Property AFJD_Id() As Integer
        Get
            Return (iAFJD_Id)
        End Get
        Set(ByVal Value As Integer)
            iAFJD_Id = Value
        End Set
    End Property
    Public Property AFJD_MasterID() As Integer
        Get
            Return (iAFJD_MasterID)
        End Get
        Set(ByVal Value As Integer)
            iAFJD_MasterID = Value
        End Set
    End Property
    Public Property AFJD_Head() As Integer
        Get
            Return (iAFJD_Head)
        End Get
        Set(ByVal Value As Integer)
            iAFJD_Head = Value
        End Set
    End Property
    Public Property AFJD_GL() As Integer
        Get
            Return (iAFJD_GL)
        End Get
        Set(ByVal Value As Integer)
            iAFJD_GL = Value
        End Set
    End Property
    Public Property AFJD_SubGL() As Integer
        Get
            Return (iAFJD_SubGL)
        End Get
        Set(ByVal Value As Integer)
            iAFJD_SubGL = Value
        End Set
    End Property
    Public Property AFJD_Debit() As Double
        Get
            Return (dAFJD_Debit)
        End Get
        Set(ByVal Value As Double)
            dAFJD_Debit = Value
        End Set
    End Property
    Public Property AFJD_Credit() As Double
        Get
            Return (dAFJD_Credit)
        End Get
        Set(ByVal Value As Double)
            dAFJD_Credit = Value
        End Set
    End Property
    Public Property AFJD_YearID() As Integer
        Get
            Return (iAFJD_YearID)
        End Get
        Set(ByVal Value As Integer)
            iAFJD_YearID = Value
        End Set
    End Property
    Public Property AFJD_CompID() As Integer
        Get
            Return (iAFJD_CompID)
        End Get
        Set(ByVal Value As Integer)
            iAFJD_CompID = Value
        End Set
    End Property
    Public Property AFJD_Status() As String
        Get
            Return (sAFJD_Status)
        End Get
        Set(ByVal Value As String)
            sAFJD_Status = Value
        End Set
    End Property
    Public Property AFJD_Operation() As String
        Get
            Return (sAFJD_Operation)
        End Get
        Set(ByVal Value As String)
            sAFJD_Operation = Value
        End Set
    End Property
    Public Property AFJD_IPAddress() As String
        Get
            Return (sAFJD_IPAddress)
        End Get
        Set(ByVal Value As String)
            sAFJD_IPAddress = Value
        End Set
    End Property


    Public Function LoadExistingVoucherNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iParty As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If iParty = 0 Then
                sSql = "Select AFJ_TransactionNo,AFJ_ID from Acc_FixedAssets_JE where AFJ_CompID=" & iCompID & " and AFJ_YearID=" & iYearID & " order by AFJ_ID Desc"
            Else
                sSql = "Select AFJ_TransactionNo,AFJ_ID from Acc_FixedAssets_JE where AFJ_CompID=" & iCompID & " and AFJ_YearID=" & iYearID & " and AFJ_Party = " & iParty & " order by AFJ_ID Desc"
            End If
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadParty(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select APM_ID,APM_Name + ' - ' + APM_Code as Name  from Accounts_Party_Master where APM_Delflag='A' and APM_CompID =" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadLocations(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iParty As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Mas_ID,Mas_Description + ' - ' + Mas_Code as Name  from sad_location_general_master where Mas_CustID = " & iParty & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadGLCodes(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAccHead As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select gl_Id, gl_glcode + '-' + gl_desc as GlDesc FROM chart_of_accounts where "
            sSql = sSql & "gl_compid=" & iCompID & " and gl_head = 2 and gl_Delflag ='C' and gl_status='A' and gl_AccHead = " & iAccHead & " order by gl_glcode"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSubGLCodes(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iglID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select gl_id, gl_glcode + '-' + gl_desc as GlDesc from chart_of_accounts where "
            sSql = sSql & "gl_compid=" & iCompID & " and gl_status='A' and gl_Delflag ='C' and gl_parent = " & iglID & " and gl_head=3"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveFixedAssetJE(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal objFAJE As clsFixedAssetJournalEntry) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(15) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFJ_ID ", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFAJE.AFJ_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFJ_TransactionNo", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objFAJE.AFJ_TransactionNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFJ_Party", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFAJE.AFJ_Party
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFJ_Location", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFAJE.AFJ_Location
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFJ_Block", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFAJE.AFJ_Block
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFJ_Amount", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objFAJE.AFJ_Amount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFJ_Narration", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objFAJE.AFJ_Narration
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFJ_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objFAJE.AFJ_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFJ_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFAJE.AFJ_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFJ_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objFAJE.AFJ_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFJ_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFAJE.AFJ_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFJ_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFAJE.AFJ_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFJ_Operation", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objFAJE.AFJ_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFJ_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objFAJE.AFJ_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spAcc_FixedAssets_JE", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveFixedAssetJEDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal objFAJE As clsFixedAssetJournalEntry) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(13) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFJD_Id", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFAJE.AFJD_Id
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFJD_MasterID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFAJE.AFJD_MasterID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFJD_Head", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFAJE.AFJD_Head
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFJD_GL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFAJE.AFJD_GL
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFJD_SubGL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFAJE.AFJD_SubGL
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFJD_Debit", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objFAJE.AFJD_Debit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFJD_Credit", OleDb.OleDbType.Double, 4)
            ObjParam(iParamCount).Value = objFAJE.AFJD_Credit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFJD_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFAJE.AFJD_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFJD_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objFAJE.AFJD_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFJD_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objFAJE.AFJD_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFJD_Operation", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objFAJE.AFJD_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFJD_IPAddress ", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objFAJE.AFJD_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spAcc_FixedAssets_JE_Details", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GenerateOrderCode(ByVal sNameSpace As String, ByVal iCompID As Integer) As String
        Dim sSql As String = "", sStr As String = "", sYear As String = ""
        Dim sMaximumID As String = ""
        Dim sMaxID As String = ""
        Dim sLastID As String = ""

        Try
            sMaximumID = objDBL.SQLGetDescription(sNameSpace, "Select Top 1 AFJ_ID From Acc_FixedAssets_JE Order By AFJ_ID Desc")
            If sMaximumID = Nothing Then
                sMaxID = "001"
            Else
                sLastID = sMaximumID + 1
                If sLastID.Length = 1 Then
                    sMaxID = "00" & "" & sLastID & ""
                ElseIf sLastID.Length = 2 Then
                    sMaxID = "00" & "" & sLastID & ""
                ElseIf sLastID.Length = 3 Then
                    sMaxID = "00" & "" & sLastID & ""
                End If
            End If

            sStr = "FAJE-" & sMaxID
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetMasterData(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iMasterID As Integer) As DataTable
        Dim dt As New DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select * From Acc_FixedAssets_JE Where AFJ_ID=" & iMasterID & " And AFJ_CompID=" & iCompID & " And AFJ_YearID=" & iYearID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindDetailsGrid(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iMasterID As Integer) As DataTable
        Dim dtTab, dt As New DataTable
        Dim dRow As DataRow
        Dim sSql As String = ""
        Try

            dtTab.Columns.Add("Sl.No")
            dtTab.Columns.Add("Head")
            dtTab.Columns.Add("GLCode")
            dtTab.Columns.Add("GLDescription")
            dtTab.Columns.Add("SubGLCode")
            dtTab.Columns.Add("SubGLDescription")
            dtTab.Columns.Add("DebitAmt(Dr.)")
            dtTab.Columns.Add("CreditAmt(Cr.)")
            dtTab.Columns.Add("Balance")
            dtTab.Columns.Add("OpeningBalance")
            dtTab.Columns.Add("PaymentType")

            sSql = "Select * From Acc_FixedAssets_JE_Details Where AFJD_masterID=" & iMasterID & " And AFJD_CompID=" & iCompID & " And AFJD_YearID=" & iYearID & " order by AFJD_ID "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)

            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtTab.NewRow()
                    dRow("Sl.No") = i + 1
                    dRow("Head") = dt.Rows(i)("AFJD_Head")
                    dRow("GLCode") = dt.Rows(i)("AFJD_GL")
                    dRow("GLDescription") = objDBL.SQLGetDescription(sNameSpace, "Select gl_glcode + '-' + gl_desc from chart_of_accounts Where gl_ID=" & dt.Rows(i)("AFJD_GL") & " And gl_CompID=" & iCompID & " ")
                    dRow("SubGLCode") = dt.Rows(i)("AFJD_SubGL")
                    dRow("SubGLDescription") = objDBL.SQLGetDescription(sNameSpace, "Select gl_glcode + '-' + gl_desc from chart_of_accounts Where gl_ID=" & dt.Rows(i)("AFJD_SubGL") & " And gl_Parent=" & dt.Rows(i)("AFJD_GL") & " And gl_CompID=" & iCompID & " ")
                    dRow("DebitAmt(Dr.)") = dt.Rows(i)("AFJD_Debit")
                    dRow("CreditAmt(Cr.)") = dt.Rows(i)("AFJD_Credit")
                    dRow("Balance") = ""
                    dRow("OpeningBalance") = ""
                    dRow("PaymentType") = ""
                    dtTab.Rows.Add(dRow)
                Next
            End If

            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
