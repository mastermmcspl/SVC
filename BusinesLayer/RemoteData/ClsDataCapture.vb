Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class ClsDataCapture
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral

    Private RATD_ID As Integer
    Private RATD_TransactionDate As Date
    Private RATD_TrType As Integer
    Private RATD_BillId As Integer
    Private RATD_PaymentType As Integer
    Private RATD_DbOrCr As Integer
    Private RATD_Head As Integer
    Private RATD_GL As Integer
    Private RATD_SubGL As Integer
    Private RATD_Debit As Decimal
    Private RATD_Credit As Decimal
    Private RATD_CreatedOn As Date
    Private RATD_CreatedBy As Integer
    Private RATD_ApprovedBy As Integer
    Private RATD_ApprovedOn As Date
    Private RATD_Deletedby As Integer
    Private RATD_DeletedOn As Date
    Private RATD_Status As String
    Private RATD_YearID As Integer
    Private RATD_Operation As String
    Private RATD_CompID As Integer
    Private RATD_IPAddress As String
    Public Structure Data
        Public DC_ID As Integer
        Public DC_TransactionNo As String
        Public DC_TrDate As DateTime
        Public DC_Company As Integer
        Public DC_Customer As Integer
        Public DC_TrType As Integer
        Public DC_BatchNo As String
        Public DC_VoucherNo As String
        Public DC_BASENAME As Integer
        Public DC_Zone As Integer
        Public DC_Region As Integer
        Public DC_Area As Integer
        Public DC_Branch As Integer
        Public DC_PaymentType As Integer
        Public DC_Narration As String
        Public DC_Delfalg As String
        Public DC_Status As String
        Public DC_CompID As Integer
        Public DC_YearID As Integer
        Public DC_CrBy As Integer
        Public DC_CrOn As DateTime
        Public DC_UpdatedBy As Integer
        Public DC_UpdatedOn As DateTime
        Public DC_Operation As String
        Public DC_IPAddress As String
    End Structure

    Public Structure Incomplete
        Public IT_ID As Integer
        Public IT_TransactionNo As String
        Public IT_TrDate As DateTime
        Public IT_Company As Integer
        Public IT_Customer As Integer
        Public IT_TrType As Integer
        Public IT_BatchNo As String
        Public IT_VoucherNo As String
        Public IT_BASENAME As Integer
        Public IT_Zone As Integer
        Public IT_Region As Integer
        Public IT_Area As Integer
        Public IT_Branch As Integer
        Public IT_PaymentType As Integer
        Public IT_Narration As String
        Public IT_Delfalg As String
        Public IT_Status As String
        Public IT_CompID As Integer
        Public IT_YearID As Integer
        Public IT_CrBy As Integer
        Public IT_CrOn As DateTime
        Public IT_UpdatedBy As Integer
        Public IT_UpdatedOn As DateTime
        Public IT_Operation As String
        Public IT_IPAddress As String
    End Structure

    Public Property sRATD_IPAddress() As String
        Get
            Return (RATD_IPAddress)
        End Get
        Set(ByVal Value As String)
            RATD_IPAddress = Value
        End Set
    End Property
    Public Property sRATD_Operation() As String
        Get
            Return (RATD_Operation)
        End Get
        Set(ByVal Value As String)
            RATD_Operation = Value
        End Set
    End Property
    Public Property iRATD_YearID() As Integer
        Get
            Return (RATD_YearID)
        End Get
        Set(ByVal Value As Integer)
            RATD_YearID = Value
        End Set
    End Property
    Public Property sRATD_Status() As String
        Get
            Return (RATD_Status)
        End Get
        Set(ByVal Value As String)
            RATD_Status = Value
        End Set
    End Property
    Public Property dRATD_DeletedOn() As Date
        Get
            Return (RATD_DeletedOn)
        End Get
        Set(ByVal Value As Date)
            RATD_DeletedOn = Value
        End Set
    End Property
    Public Property iRATD_Deletedby() As Integer
        Get
            Return (RATD_Deletedby)
        End Get
        Set(ByVal Value As Integer)
            RATD_Deletedby = Value
        End Set
    End Property
    Public Property dRATD_ApprovedOn() As Date
        Get
            Return (RATD_ApprovedOn)
        End Get
        Set(ByVal Value As Date)
            RATD_ApprovedOn = Value
        End Set
    End Property
    Public Property iRATD_ApprovedBy() As Integer
        Get
            Return (RATD_ApprovedBy)
        End Get
        Set(ByVal Value As Integer)
            RATD_ApprovedBy = Value
        End Set
    End Property
    Public Property dRATD_CreatedOn() As Date
        Get
            Return (RATD_CreatedOn)
        End Get
        Set(ByVal Value As Date)
            RATD_CreatedOn = Value
        End Set
    End Property
    Public Property iRATD_CreatedBy() As Integer
        Get
            Return (RATD_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            RATD_CreatedBy = Value
        End Set
    End Property
    Public Property dRATD_Credit() As Decimal
        Get
            Return (RATD_Credit)
        End Get
        Set(ByVal Value As Decimal)
            RATD_Credit = Value
        End Set
    End Property
    Public Property dRATD_Debit() As Decimal
        Get
            Return (RATD_Debit)
        End Get
        Set(ByVal Value As Decimal)
            RATD_Debit = Value
        End Set
    End Property
    Public Property iRATD_SubGL() As Integer
        Get
            Return (RATD_SubGL)
        End Get
        Set(ByVal Value As Integer)
            RATD_SubGL = Value
        End Set
    End Property
    Public Property iRATD_GL() As Integer
        Get
            Return (RATD_GL)
        End Get
        Set(ByVal Value As Integer)
            RATD_GL = Value
        End Set
    End Property
    Public Property iRATD_Head() As Integer
        Get
            Return (RATD_Head)
        End Get
        Set(ByVal Value As Integer)
            RATD_Head = Value
        End Set
    End Property
    Public Property iRATD_CompID() As Integer
        Get
            Return (RATD_CompID)
        End Get
        Set(ByVal Value As Integer)
            RATD_CompID = Value
        End Set
    End Property
    Public Property iRATD_DbOrCr() As Integer
        Get
            Return (RATD_DbOrCr)
        End Get
        Set(ByVal Value As Integer)
            RATD_DbOrCr = Value
        End Set
    End Property
    Public Property iRATD_PaymentType() As Integer
        Get
            Return (RATD_PaymentType)
        End Get
        Set(ByVal Value As Integer)
            RATD_PaymentType = Value
        End Set
    End Property
    Public Property iRATD_BillId() As Integer
        Get
            Return (RATD_BillId)
        End Get
        Set(ByVal Value As Integer)
            RATD_BillId = Value
        End Set
    End Property
    Public Property iRATD_TrType() As Integer
        Get
            Return (RATD_TrType)
        End Get
        Set(ByVal Value As Integer)
            RATD_TrType = Value
        End Set
    End Property
    Public Property dRATD_TransactionDate() As Date
        Get
            Return (RATD_TransactionDate)
        End Get
        Set(ByVal Value As Date)
            RATD_TransactionDate = Value
        End Set
    End Property
    Public Property iRATD_ID() As Integer
        Get
            Return (RATD_ID)
        End Get
        Set(ByVal Value As Integer)
            RATD_ID = Value
        End Set
    End Property

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
            sSql = "Select Org_Node,Org_Name From Sad_Org_Structure Where Org_Parent=" & iAccZone & " And Org_CompID=" & iCompID & " "
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
            sSql = "Select Org_Node,Org_Name From Sad_Org_Structure Where Org_Parent=" & iAccRgn & " And Org_CompID=" & iCompID & " "
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
            sSql = "Select Org_Node,Org_Name From Sad_Org_Structure Where Org_Parent=" & iAccarea & " And Org_CompID=" & iCompID & " "
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
            sSql = sSql & "gl_compid=" & iCompID & " and gl_status='A' and gl_Delflag ='C' and gl_parent = " & iglID & " and gl_head=3 "
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
    Public Function LoadSubGLDetails(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select gl_id, gl_glcode + '-' + gl_desc as GlDesc from chart_of_accounts where "
            sSql = sSql & "gl_compid=" & iCompID & " and gl_status='A' and gl_Delflag ='C' and gl_head=3 order by gl_AccHead"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetchartofAccounts(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from chart_of_Accounts where gl_CompID=" & iCompID & " and gl_DelFlag ='C'"
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BuildTable() As DataTable
        Dim dt As New DataTable
        Dim dc As New DataColumn
        Try
            dc = New DataColumn("ID", GetType(Integer))
            dt.Columns.Add(dc)
            dc = New DataColumn("HeadID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("GLID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("SubGLID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("GLCode", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("GLDescription", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("SubGL", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("SubGLDescription", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("OpeningBalance", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Debit", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Credit", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Balance", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("DebitORCredit", GetType(Integer))
            dt.Columns.Add(dc)

            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadPaymentsMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iHead As Integer, ByVal iGLID As Integer, ByVal iSubGL As Integer, ByVal dAmount As Double, ByVal iDbOrCr As Integer, ByVal dtPayment As DataTable, ByVal dtCOA As DataTable) As DataTable
        Dim dt As New DataTable
        Dim dr As DataRow
        Try
            dt = BuildTable()

            dr = dt.NewRow
            dr("HeadID") = iHead
            dr("GLID") = iGLID
            dr("SubGLID") = iSubGL
            dr("DebitORCredit") = iDbOrCr

            If iGLID > 0 Then
                Dim dtDGL As New DataTable
                Dim DVGLCODE As New DataView(dtCOA)
                DVGLCODE.RowFilter = "Gl_id=" & iGLID
                dtDGL = DVGLCODE.ToTable

                dr("GLCode") = dtDGL.Rows(0)("gl_glcode")
                dr("GLDescription") = dtDGL.Rows(0)("gl_desc")

            Else
                dr("GLCode") = "" : dr("GLDescription") = "" : dr("Debit") = "0.00" : dr("Credit") = "0.00" : dr("GLID") = "0"
            End If


            If iSubGL > 0 Then
                Dim dtDSUBGL As New DataTable
                Dim DVSUBGLCODE As New DataView(dtCOA)
                DVSUBGLCODE.RowFilter = "Gl_id=" & iSubGL
                dtDSUBGL = DVSUBGLCODE.ToTable

                dr("SubGL") = dtDSUBGL.Rows(0)("gl_glcode")
                dr("SubGLDescription") = dtDSUBGL.Rows(0)("gl_desc")
            Else
                dr("SubGL") = "" : dr("SubGLDescription") = "" : dr("Debit") = "0.00" : dr("Credit") = "0.00" : dr("SubGLID") = "0"
            End If


            Dim iCount As Integer = 0
            iCount = dtPayment.Rows.Count + 1

            If iDbOrCr = 1 Then
                dr("ID") = iCount
                If iSubGL > 0 Then
                    dr("OpeningBalance") = GetOpeningBalance(sNameSpace, iCompID, iYearID, "Opn_DebitAmt", iSubGL)
                Else
                    dr("OpeningBalance") = GetOpeningBalance(sNameSpace, iCompID, iYearID, "Opn_DebitAmt", iGLID)
                End If

                dr("Debit") = dAmount
                dr("Credit") = 0.00
                dr("DebitOrCredit") = 1
            Else
                dr("ID") = iCount
                If iSubGL > 0 Then
                    dr("OpeningBalance") = GetOpeningBalance(sNameSpace, iCompID, iYearID, "Opn_CreditAmount", iSubGL)
                Else
                    dr("OpeningBalance") = GetOpeningBalance(sNameSpace, iCompID, iYearID, "Opn_CreditAmount", iGLID)
                End If
                dr("Debit") = 0.00
                dr("Credit") = dAmount
                dr("DebitOrCredit") = 2
            End If
            dt.Rows.Add(dr)

            If dtPayment.Rows.Count > 0 Then
                dtPayment.Merge(dt, True, MissingSchemaAction.Ignore)
            Else
                dtPayment.Merge(dt)
            End If
            'dtPayment.Merge(dt)
            Return dtPayment
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetOpeningBalance(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sColumn As String, ByVal iGlID As Integer) As Double
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dDebitOrCredit As Double = 0
        Try
            sSql = "" : sSql = "Select " & sColumn & " from acc_Opening_Balance where Opn_GLID =" & iGlID & " and Opn_YearID =" & iYearID & " and Opn_CompID =" & iCompID & ""
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                dDebitOrCredit = dt.Rows(0)(sColumn).ToString()
            End If
            Return dDebitOrCredit
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCabID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sCust As String) As Integer
        Dim sSql As String = ""
        Try
            sSql = "" : sSql = "select CBN_NODE from EDT_CABINET where CBN_NAME='" & sCust & "'"
            Return objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSubCabID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sSub As String, ByVal iCabId As Integer) As Integer
        Dim sSql As String = ""
        Dim iRet As Integer
        Try
            sSql = "" : sSql = "select CBN_NODE from EDT_CABINET where CBN_NAME='" & sSub & "' and CBN_PARENT=" & iCabId & " "
            iRet = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return iRet
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFoldID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sFold As String, ByVal iSubCabId As Integer) As Integer
        Dim sSql As String = ""
        Dim iRet As Integer
        Try
            sSql = "" : sSql = "select FOL_FOLID from EDT_FOLDER where FOL_NAME='" & sFold & "' and FOL_CABINET=" & iSubCabId & " "
            iRet = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return iRet
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadFileNames(ByVal sAC As String, ByVal iBaseID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select pge_basename,pge_OrignalFileName from EDT_PAGE where pge_status <> 'X' and Pge_Details_Id=" & iBaseID & " ORDER BY PGE_PAGENO"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadUser(ByVal sAC As String, ByVal iACId As Integer, ByVal iUserID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Usr_ID,Usr_FullName from sad_userdetails where Usr_CompID=" & iACId & " and USR_DelFlag='A'"
            If iUserID > 0 Then
                sSql = sSql & " And Usr_ID !=" & iUserID & ""
            End If
            sSql = sSql & " order by Usr_FullName"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetEmailSentUserID(ByVal sAC As String, ByVal iACId As Integer, ByVal sUserMailID As String) As Integer
        Dim sSql As String = ""
        Dim iUserID As Integer = 0
        Try
            sSql = "Select Case When usr_Email Is Null then 0 else usr_Email End As usr_Email from sad_USERDETAILS Where Usr_CompID=" & iACId & " and USR_DelFlag='A' And usr_Email='" & sUserMailID & "'"
            iUserID = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return iUserID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadUserEmailIDs(ByVal sAC As String, ByVal iACId As Integer, ByVal sUserID As String) As String
        Dim sSql As String = "", sEmailIds As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Case When usr_Email Is NULL Then '' else usr_Email End As usr_Email From Sad_UserDetails Where"
            If sUserID <> "" Then
                sSql = sSql & " Usr_ID IN (" & sUserID & ") And"
            End If
            sSql = sSql & " Usr_CompID=" & iACId & " and USR_DelFlag='A'"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)

            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    sEmailIds = sEmailIds & "," & dt.Rows(i)("usr_Email")
                Next
            End If
            Return sEmailIds
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetLoginUserEmailID(ByVal sAC As String, ByVal iACId As Integer, ByVal iUserID As Integer) As String
        Dim sSql As String = "", sEmailId As String = ""
        Try
            sSql = "Select Case When usr_Email Is NULL then '' else usr_Email End As usr_Email From Sad_UserDetails where Usr_CompID=" & iACId & " and USR_DelFlag='A'"
            If iUserID > 0 Then
                sSql = sSql & " And Usr_ID=" & iUserID & ""
            End If
            sSql = sSql & " order by Usr_FullName"
            sEmailId = objDBL.SQLExecuteScalar(sAC, sSql)
            Return sEmailId
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub SaveSentEmailDetails(ByVal sAC As String, ByVal iMstPKID As Integer, ByVal iYearID As Integer, ByVal sFormName As String, ByVal sFromEmailID As String,
                                    ByVal sToEmailIDs As String, ByVal sCCDetails As String, ByVal sSubject As String, ByVal sBody As String, ByVal sIsMailSent As String,
                                    ByVal sAttachedPath As String, ByVal sAttachedDocIDs As String, ByVal iEmailSentUserID As Integer, ByVal iUserID As Integer,
                                    ByVal sIPaddress As String, ByVal iCompID As Integer)
        Dim sSql As String = ""
        Dim iPKID As Integer = 0
        Try
            iPKID = objDBL.SQLExecuteScalarInt(sAC, "Select IsNull(Max(EMD_ID),0)+1 from GRACe_EMailSent_Details")
            sSql = "Insert Into GRACe_EMailSent_Details(EMD_ID,EMD_MstPKID,EMD_YearID,EMD_FormName,EMD_FromEmailID,EMD_ToEmailIDs,EMD_CCEmailIDs,"
            sSql = sSql & " EMD_Subject,EMD_Body,EMD_EMailStatus,EMD_SentUsrID,EMD_SentOn,EMD_CreatedBy,EMD_CreatedOn,EMD_AttachedPath,"
            sSql = sSql & " EMD_AttachedDocIDs,EMD_IPAddress,EMD_CompID) Values(" & iPKID & ",'" & iMstPKID & "',"
            sSql = sSql & " " & iYearID & ",'" & sFormName & "','" & sFromEmailID & "','" & sToEmailIDs & "','" & sCCDetails & "',"
            sSql = sSql & " '" & sSubject & "','" & sBody & "','" & sIsMailSent & "'," & iEmailSentUserID & ",GetDate()," & iUserID & ",GetDate(),'" & sAttachedPath & "',"
            sSql = sSql & " '" & sAttachedDocIDs & "','" & sIPaddress & "'," & iCompID & ")"
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub UpdateEdtPageStatus(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iEdtPagePkid As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Update EDT_PAGE set PGE_STATUS='S',Pge_UpdatedBy=" & iUserID & ",Pge_UpdatedOn=Getdate() where PGE_BASENAME=" & iEdtPagePkid & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function SaveDataCapture(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal objDC As ClsDataCapture.Data) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(26) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DC_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDC.DC_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DC_TransactionNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objDC.DC_TransactionNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DC_TrDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objDC.DC_TrDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DC_Company", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDC.DC_Company
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DC_Customer", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDC.DC_Customer
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DC_TrType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDC.DC_TrType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DC_BatchNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objDC.DC_BatchNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DC_VoucherNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objDC.DC_VoucherNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DC_BASENAME", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDC.DC_BASENAME
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DC_Zone", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDC.DC_Zone
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DC_Region", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDC.DC_Region
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DC_Area", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDC.DC_Area
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DC_Branch", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDC.DC_Branch
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DC_PaymentType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDC.DC_PaymentType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DC_Narration", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objDC.DC_Narration
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DC_Delfalg", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objDC.DC_Delfalg
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DC_Status", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objDC.DC_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DC_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDC.DC_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DC_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDC.DC_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DC_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDC.DC_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DC_CrOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objDC.DC_CrOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DC_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDC.DC_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SPO_UpdatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objDC.DC_UpdatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DC_Operation", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objDC.DC_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DC_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objDC.DC_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spData_Capture", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iCompany As Integer, ByVal iTrType As Integer, ByVal iBatchNo As Integer, ByVal iBaseNameID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            'sSql = "Select * From Data_Capture Where DC_Company=" & iCompany & " And DC_TrType=" & iTrType & " And DC_BatchNO=" & iBatchNo & " And DC_BaseName=" & iBaseNameID & " And DC_CompID=" & iCompID & " And DC_YearID=" & iYearID & " "
            sSql = "Select * From Data_Capture Where DC_Company=" & iCompany & " And DC_TrType=" & iTrType & " And DC_BatchNO=" & iBatchNo & " And DC_CompID=" & iCompID & " And DC_YearID=" & iYearID & " "
            GetDetails = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return GetDetails
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindTrType(sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iCustomerID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select CBN_NODE,CBN_NAME From EDT_Cabinet Where CBN_Parent<>-1 And CBN_PARENT=" & iCustomerID & " "
            BindTrType = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindBatchNo(sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iSubCabinetID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select FOL_FOLID,FOL_NAME From EDT_Folder Where FOL_CABINET=" & iSubCabinetID & " "
            BindBatchNo = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GenerateOrderCode(ByVal sNameSpace As String, ByVal iCompID As Integer) As String
        Dim sSql As String = "" : Dim sStr As String = ""
        Dim sMaximumID As String = ""
        Try
            sMaximumID = objDBL.SQLGetDescription(sNameSpace, "Select Top 1 DC_ID From Data_Capture Order By DC_ID Desc")
            sStr = "DC-" & sMaximumID + 1
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindPaymentType(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Mas_ID,Mas_Desc From acc_general_master Where Mas_Master In (Select Mas_ID From acc_master_Type Where Mas_Type='Payment Type') And Mas_CompID=" & iCompID & " and Mas_Delflag='A' "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveTransactionDetails(ByVal sAC As String, ByVal iCompID As Integer, ByVal iPaymentType As Integer, ByVal objDataCap As ClsDataCapture) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(18) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RATD_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDataCap.iRATD_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RATD_TransactionDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objDataCap.dRATD_TransactionDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RATD_TrType ", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDataCap.iRATD_TrType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RATD_BillID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDataCap.iRATD_BillId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RATD_PaymentType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDataCap.iRATD_PaymentType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RATD_Head", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDataCap.iRATD_Head
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RATD_GL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDataCap.iRATD_GL
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RATD_SubGL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDataCap.iRATD_SubGL
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RATD_DbOrCr", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDataCap.iRATD_DbOrCr
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RATD_Debit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objDataCap.dRATD_Debit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RATD_Credit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objDataCap.dRATD_Credit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDataCap.iRATD_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RATD_Status", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objDataCap.sRATD_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RATD_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDataCap.iRATD_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RATD_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDataCap.iRATD_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RATD_Operation ", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objDataCap.sRATD_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RATD_IPAddress ", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objDataCap.sRATD_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "SPRemote_Acc_Transactions_Details", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSavedTransactionDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iPaymentID As Integer, ByVal iTrType As Integer) As DataTable
        Dim dt As New DataTable
        Dim ds As New DataSet
        Dim dc As New DataColumn
        Dim sSql As String = "", aSql As String = ""
        Dim dr As DataRow
        Dim i As Integer = 0
        Try

            dc = New DataColumn("ID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("HeadID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("GLID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("SubGLID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("GLCode", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("GLDescription", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("SubGL", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("SubGLDescription", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("OpeningBalance", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Debit", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Credit", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Balance", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("DebitOrCredit", GetType(String))
            dt.Columns.Add(dc)

            sSql = "select A.RATD_ID,A.RATD_Head,A.RATD_PaymentType,A.RATD_Gl,A.RATD_SubGL,A.RATD_Debit,A.RATD_DbOrCr,A.RATD_Credit,B.gl_glCode as GlCode, B.gl_Desc as GlDescription, C.gl_glCode as SubGlCode, c.Gl_Desc as SubGlDesc
                    from Remote_Acc_Transactions_Details A 
                    join chart_of_Accounts B on B.gl_ID =A.RATD_GL
                    Left join chart_of_Accounts C on  C.gl_ID =A.RATD_SubGL 
                    where A.RATD_BillId = 1 and A.RATD_TRType = 2 And A.RATD_YearID=4 order by a.RAtd_id "

            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dr = dt.NewRow
                    dr("ID") = ds.Tables(0).Rows(i)("RATD_ID")

                    If IsDBNull(ds.Tables(0).Rows(i)("RATD_Head").ToString()) = False Then
                        dr("HeadID") = ds.Tables(0).Rows(i)("RATD_Head").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("RATD_GL").ToString()) = False Then
                        dr("GLID") = ds.Tables(0).Rows(i)("RATD_GL").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("RATD_SubGL").ToString()) = False Then
                        dr("SubGLID") = ds.Tables(0).Rows(i)("RATD_SubGL").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("GLCode").ToString()) = False Then
                        dr("GLCode") = ds.Tables(0).Rows(i)("GLCode").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("GLDescription").ToString()) = False Then
                        dr("GLDescription") = ds.Tables(0).Rows(i)("GLDescription").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("SubGLCode").ToString()) = False Then
                        dr("SubGL") = ds.Tables(0).Rows(i)("SubGLCode").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("SubGLDesc").ToString()) = False Then
                        dr("SubGLDescription") = ds.Tables(0).Rows(i)("SubGLDesc").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("RATD_Debit").ToString()) = False Then
                        dr("Debit") = Convert.ToDecimal(ds.Tables(0).Rows(i)("RATD_Debit").ToString()).ToString("#,##0.00")
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("RATD_Credit").ToString()) = False Then
                        dr("Credit") = Convert.ToDecimal(ds.Tables(0).Rows(i)("RATD_Credit").ToString()).ToString("#,##0.00")
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("RATD_DbOrCr").ToString()) = False Then
                        dr("DebitOrCredit") = ds.Tables(0).Rows(i)("RATD_DbOrCr")
                    End If

                    dt.Rows.Add(dr)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindExistingNo(sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iCabinetID As Integer, ByVal iSubCabinetID As Integer, ByVal iFolderID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select DC_ID,DC_TransactionNo From Data_Capture Where DC_Company=" & iCabinetID & " And DC_TrType=" & iSubCabinetID & " And DC_BatchNo=" & iFolderID & " And DC_CompID=" & iCompID & " And DC_YearID=" & iYearID & " "
            BindExistingNo = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function ApproveDC(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iCabinetID As Integer, ByVal iSubCabinetID As Integer, ByVal iFolderID As Integer, ByVal iDCID As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Update Data_Capture Set DC_Status='A' Where DC_ID=" & iDCID & " And DC_Customer=" & iCabinetID & " And DC_TrType=" & iSubCabinetID & " And DC_BatchNo=" & iFolderID & " And DC_CompID=" & iCompID & " And DC_YearID=" & iYearID & " "
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function DeleteTransaction(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iMasterID As Integer, ByVal iTrTypeID As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Delete From Remote_Acc_Transactions_Details Where RATD_BillID=" & iMasterID & " And RATD_TrType=" & iTrTypeID & " And RATD_YearID=" & iYearID & " "
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveIncompleteDataCapture(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal objDC As ClsDataCapture.Incomplete) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(24) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@IT_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDC.IT_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@IT_TransactionNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objDC.IT_TransactionNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@IT_TrDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objDC.IT_TrDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@IT_Company", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDC.IT_Company
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@IT_Customer", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDC.IT_Customer
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@IT_TrType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDC.IT_TrType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@IT_BatchNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objDC.IT_BatchNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@IT_VoucherNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objDC.IT_VoucherNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@IT_BASENAME", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDC.IT_BASENAME
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@IT_Zone", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDC.IT_Zone
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@IT_Region", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDC.IT_Region
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@IT_Area", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDC.IT_Area
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@IT_Branch", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDC.IT_Branch
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@IT_PaymentType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDC.IT_PaymentType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@IT_Narration", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objDC.IT_Narration
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@IT_Delfalg", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objDC.IT_Delfalg
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@IT_Status", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objDC.IT_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@IT_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDC.IT_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@IT_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDC.IT_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@IT_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDC.IT_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@IT_CrOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objDC.IT_CrOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@IT_Operation", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objDC.IT_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@IT_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objDC.IT_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spRemote_Incomplete_Transactions", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCompany(sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            'sSql = "Select BM_ID,BM_Code + ' - ' + BM_Name as Name  from sales_Buyers_Masters where BM_DelFlag='A' and BM_CompID =" & iCompID & ""
            sSql = "Select CBN_NODE,CBN_NAME as Name from EDT_Cabinet where CBN_Parent=-1 And CBN_DelStatus='A' "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCustomerSupplier(sNameSpace As String, ByVal iCompID As Integer, ByVal sStr As String) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If sStr = "Purchase" Or sStr = "Payment" Or sStr = "Cash Purchase" Or sStr = "Purchase Return" Or sStr = "GIN" Then
                sSql = "Select CSM_ID As ID ,CSM_Code + ' - ' + CSM_Name as Name from CustomerSupplierMaster where CSM_DelFlag='A' and CSM_CompID =" & iCompID & ""
                dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            ElseIf sStr = "Sales" Or sStr = "Receipt" Or sStr = "Cash Sales" Or sStr = "Sales Dispatch" Or sStr = "Sales Invoice" Or sStr = "Sales Return" Then
                sSql = "Select BM_ID As ID ,BM_Code + ' - ' + BM_Name as Name from sales_Buyers_Masters where BM_DelFlag='A' and BM_CompID =" & iCompID & ""
                dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            End If
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
    Public Function LoadPaymentsMaster1(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iHead As Integer, ByVal iGLID As Integer, ByVal iSubGL As Integer, ByVal dAmount As Double, ByVal iDbOrCr As Integer, ByVal dtPayment As DataTable, ByVal dtCOA As DataTable) As DataTable
        Dim dt As New DataTable
        Dim dr As DataRow
        Try
            dt = BuildTable1()

            dr = dt.NewRow
            dr("HeadID") = iHead
            dr("GLID") = iGLID
            dr("SubGLID") = iSubGL
            If iGLID > 0 Then
                Dim dtDGL As New DataTable
                Dim DVGLCODE As New DataView(dtCOA)
                DVGLCODE.RowFilter = "Gl_id=" & iGLID
                dtDGL = DVGLCODE.ToTable

                dr("GLCode") = dtDGL.Rows(0)("gl_glcode")
                dr("GLDescription") = dtDGL.Rows(0)("gl_desc")

            Else
                dr("GLCode") = "" : dr("GLDescription") = "" : dr("Debit") = "0.00" : dr("Credit") = "0.00" : dr("GLID") = "0"
            End If


            If iSubGL > 0 Then
                Dim dtDSUBGL As New DataTable
                Dim DVSUBGLCODE As New DataView(dtCOA)
                DVSUBGLCODE.RowFilter = "Gl_id=" & iSubGL
                dtDSUBGL = DVSUBGLCODE.ToTable

                dr("SubGL") = dtDSUBGL.Rows(0)("gl_glcode")
                dr("SubGLDescription") = dtDSUBGL.Rows(0)("gl_desc")
            Else
                dr("SubGL") = "" : dr("SubGLDescription") = "" : dr("Debit") = "0.00" : dr("Credit") = "0.00" : dr("SubGLID") = "0"
            End If


            Dim iCount As Integer = 0
            iCount = dtPayment.Rows.Count + 1

            If iDbOrCr = 1 Then
                dr("ID") = iCount
                If iSubGL > 0 Then
                    dr("OpeningBalance") = GetOpeningBalance(sNameSpace, iCompID, iYearID, "Opn_DebitAmt", iSubGL)
                Else
                    dr("OpeningBalance") = GetOpeningBalance(sNameSpace, iCompID, iYearID, "Opn_DebitAmt", iGLID)
                End If

                dr("Debit") = dAmount
                dr("Credit") = 0.00
            Else
                dr("ID") = iCount
                If iSubGL > 0 Then
                    dr("OpeningBalance") = GetOpeningBalance(sNameSpace, iCompID, iYearID, "Opn_CreditAmount", iSubGL)
                Else
                    dr("OpeningBalance") = GetOpeningBalance(sNameSpace, iCompID, iYearID, "Opn_CreditAmount", iGLID)
                End If
                dr("Debit") = 0.00
                dr("Credit") = dAmount
            End If
            dt.Rows.Add(dr)

            If dtPayment.Rows.Count > 0 Then
                dtPayment.Merge(dt, True, MissingSchemaAction.Ignore)
            Else
                dtPayment.Merge(dt)
            End If

            Return dtPayment
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BuildTable1() As DataTable
        Dim dt As New DataTable
        Dim dc As New DataColumn
        Try
            dc = New DataColumn("ID", GetType(Integer))
            dt.Columns.Add(dc)
            dc = New DataColumn("HeadID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("GLID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("SubGLID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("SrNo", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("GLCode", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("GLDescription", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("SubGL", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("SubGLDescription", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("OpeningBalance", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Debit", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Credit", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Balance", GetType(String))
            dt.Columns.Add(dc)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
