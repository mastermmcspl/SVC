Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class ClsPettyCashDayBook
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions
    Private Shared sSession As AllSession
    Public iPdb_pkid As Integer
    Public sPdb_transactionNo As String
    Public iPDB_ZoneID As Integer
    Public iPDB_RegionID As Integer
    Public iPDB_AreaID As Integer
    Public iPDB_BranchID As Integer
    Public sPdb_Narration As String
    Public dPdb_cashTotal As Double
    Public dPdb_DebitTotal As Double
    Public iPDB_YearId As Integer
    Public iPDB_CompID As Integer
    Public sPDB_Status As String
    Public sPDB_IPAddress As String
    Public iPDB_UpdatedBy As Integer
    Public iPDB_CreatedBy As Integer



    'MasterDetails
    Public iPDBDD_PKID As Integer
    Public iPDBD_MasterID As Integer
    Public dPDBD_CashReceived As Double
    Public dPDBD_Date As Date
    Public sPDBD_Particulars As String
    Public sPDBD_Voucherno As String
    Public dPDBD_DebitAmount As Double
    Public iPDBD_CreatedBy As Integer
    Public iPDBD_YearID As Integer
    Public iPDBD_CompID As Integer
    Public sPDBD_Status As String
    Public sPDBD_IPAddress As String
    Public iPDBD_UpdatedBy As Integer
    Public sPDBD_Narration As String


    'Account Transaction Details
    Public iATD_ID As Integer
    Public dATD_TransactionDate As Date
    Public iATD_TrType As Integer
    Public iATD_BillId As Integer
    Public iATD_PaymentType As Integer
    Public iATD_Head As Integer
    Public iATD_DbOrCr As Integer
    Public iATD_GL As Integer
    Public iATD_SubGL As Integer
    Public dATD_Debit As Decimal
    Public dATD_Credit As Decimal
    Public dATD_CreatedOn As Date
    Public iATD_CreatedBy As Integer
    Public dATD_UpdatedOn As Date
    Public iATD_UpdatedBy As Integer
    Public iATD_ApprovedBy As Integer
    Public dATD_ApprovedOn As Date
    Public iATD_Deletedby As Integer
    Public dATD_DeletedOn As Date
    Public sATD_Status As String
    Public iATD_YearID As Integer
    Public sATD_Operation As String
    Public sATD_IPAddress As String

    Public iATD_ZoneID As Integer
    Public iATD_RegionID As Integer
    Public iATD_AreaID As Integer
    Public iATD_BranchID As Integer

    Public dATD_OpenDebit As Decimal
    Public dATD_OpenCredit As Decimal
    Public dATD_ClosingDebit As Decimal
    Public dATD_ClosingCredit As Decimal
    Public iATD_SeqReferenceNum As Integer

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
    Public Function LoadExistingPettyCashVoucherNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try

            sSql = "Select PDB_TransactionNo,PDB_PKID from  ACC_PettyCashDayBookMaster where PDB_CompID=" & iCompID & " and PDB_YearID=" & iYearID & " order by PDB_PKID Desc"


            'sSql = "Select PDB_TransactionNo,PDB_PKID from  ACC_PettyCashDayBookMaster where PDB_CompID=" & iCompID & " and PDB_YearID=" & iYearID & " and pdb_pkid=" & iParty & "  order by PDB_PKID Desc"


            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllGLCodes(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select gl_Id, gl_glcode + '-' + gl_desc as GlDesc FROM chart_of_accounts where "
            sSql = sSql & "gl_compid=" & iCompID & "  and gl_head = 2 and gl_Delflag ='C' and gl_status='A' order by gl_glcode"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GenerateTransactionNumber(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As String
        Dim sSql As String = "", sPrefix As String = ""
        Dim iMax As Integer = 0
        Dim ds As New DataSet
        Try
            iMax = objDBL.SQLExecuteScalar(sNameSpace, "Select isnull(Count(PDB_PKID)+1,1) from ACC_PettyCashDayBookMaster Where PDB_YearID=" & iYearID & " And PDB_CompID=" & iCompID & " ")

            sSql = "" : sSql = "Select * from ACC_Voucher_Settings where AVS_TransType = 12  and AVS_CompID = " & iCompID & ""
            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            If ds.Tables(0).Rows.Count > 0 Then
                sPrefix = ds.Tables(0).Rows(0)("AVS_Prefix").ToString() & "00" & iMax
            Else
                sPrefix = ""
            End If
            Return sPrefix
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSubGLCodes(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iGlid As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select gl_Id, gl_glcode + '-' + gl_desc as GlDesc FROM chart_of_accounts where "
            sSql = sSql & "gl_compid=" & iCompID & "  and gl_head = 3 and gl_Parent=" & iGlid & " and gl_Delflag ='C' and gl_status='A' order by gl_glcode"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSubGLCodes(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iGlid As Integer) As String
        Dim sSql As String = ""
        Dim sSGLCodeDesc As String
        Try
            sSql = "" : sSql = "Select  gl_glcode + '-' + gl_desc as GlDesc FROM chart_of_accounts where "
            sSql = sSql & "gl_compid=" & iCompID & "  and gl_head = 3 and gl_Parent=" & iGlid & " and gl_Delflag ='C' and gl_status='A' order by gl_glcode"
            sSGLCodeDesc = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return sSGLCodeDesc
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPaymentDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPaymentID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from ACC_PettyCashDayBookMaster where PDB_PKID =" & iPaymentID & " and PDB_CompID =" & iCompID & ""
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadTransactionDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iPaymentID As Integer) As DataTable
        Dim dt As New DataTable
        Dim ds As New DataSet
        Dim dc As New DataColumn
        Dim sSql As String = "", aSql As String = ""
        Dim dr As DataRow
        Dim sAc As String
        Dim i As Integer = 0
        Dim iSlno As Integer = 0

        Dim sDate As Date
        Try

            dc = New DataColumn("SrNo", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("PkId", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("MasterId", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Date", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Particulars", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("ParticularsId", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("CashRecieved", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("VoucherNo", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Amount", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Narration", GetType(String))
            dt.Columns.Add(dc)

            ' dt.Columns.Add(dc)



            'sSql = "" : sSql = "select A.pdb_PKID,A.pdb_TransactionNo,A.ATD_SubGL,A.ATD_Debit,A.ATD_Credit,B.gl_glCode as GlCode,"
            'sSql = sSql & "B.gl_Desc as GlDescription, C.gl_glCode as SubGlCode, c.Gl_Desc as SubGlDesc "
            'sSql = sSql & "from Acc_Transactions_Details A join chart_of_Accounts B on A.ATD_BillId = " & iPaymentID & " and A.ATD_TrType = 2 "
            'sSql = sSql & "and A.ATD_GL = B.gl_ID Left join chart_of_Accounts C on A.ATD_SubGL = C.gl_ID and A.ATD_BillId = " & iPaymentID & " order by a.Atd_id"

            sSql = "" : sSql = " select A.pdb_PKID,b.pdbdd_pkid,b.pdbd_masterid,b.pdbd_date,b.PDBD_CashReceived,b.PDBD_PArticulars,b.pdbd_debitamount,b.pdbd_voucherno,b.pdbd_Narration "
            sSql = sSql & " from ACC_PettyCashDayBookMaster A join ACC_PettyCashDayBookMaster_Details b on A.PDB_PKID=b.PDBD_MasterId "
            sSql = sSql & " where a.pdb_pkid=" & iPaymentID & " order by b.PDBdd_PKID"

            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dr = dt.NewRow

                    If IsDBNull(ds.Tables(0).Rows(i)("pdbdd_pkid").ToString()) = False Then
                        dr("PkId") = ds.Tables(0).Rows(i)("pdbdd_pkid").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("pdb_PKID").ToString()) = False Then
                        dr("MasterId") = ds.Tables(0).Rows(i)("pdb_PKID").ToString()
                    End If



                    If IsDBNull(ds.Tables(0).Rows(i)("pdbd_date").ToString()) = False Then
                        sDate = ds.Tables(0).Rows(i)("pdbd_date").ToString()
                        dr("Date") = Convert.ToString(sDate.ToString("dd/MM/yyyy"))
                    End If

                    'If IsDBNull(ds.Tables(0).Rows(i)("ATD_PaymentType").ToString()) = False Then
                    '    dr("PaymentID") = ds.Tables(0).Rows(i)("ATD_PaymentType").ToString()

                    '    If ds.Tables(0).Rows(i)("ATD_PaymentType").ToString() = "1" Then
                    '        dr("Type") = "Advance Payment"
                    '    ElseIf ds.Tables(0).Rows(i)("ATD_PaymentType").ToString() = "2" Then
                    '        dr("Type") = "Bill Passing"
                    '    ElseIf ds.Tables(0).Rows(i)("ATD_PaymentType").ToString() = "3" Then
                    '        dr("Type") = "Payment"
                    '    ElseIf ds.Tables(0).Rows(i)("ATD_PaymentType").ToString() = "4" Then
                    '        dr("Type") = "Cheque"
                    '    End If
                    'End If

                    If IsDBNull(ds.Tables(0).Rows(i)("PDBD_PArticulars").ToString()) = False Then
                        dr("Particulars") = LoadPereticulars(sNameSpace, (ds.Tables(0).Rows(i)("PDBD_PArticulars")))
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("PDBD_PArticulars").ToString()) = False Then
                        dr("ParticularsId") = ds.Tables(0).Rows(i)("PDBD_PArticulars").ToString()
                    End If
                    'If IsDBNull(ds.Tables(0).Rows(i)("GLDescription").ToString()) = False Then
                    '    dr("ParticularsId") = ds.Tables(0).Rows(i)("GLDescription").ToString()

                    'End If


                    'If IsDBNull(ds.Tables(0).Rows(i)("SubGLCredit").ToString()) = False Then
                    '    If ds.Tables(0).Rows(i)("SubGLCredit").ToString() <> "0.00" Then
                    '        dr("OpeningBalance") = ds.Tables(0).Rows(i)("SubGLCredit").ToString()
                    '    End If
                    'End If


                    If IsDBNull(ds.Tables(0).Rows(i)("PDBD_CashReceived").ToString()) = False Then
                        If ds.Tables(0).Rows(i)("PDBD_CashReceived").ToString() <> 0.0000 Then
                            iSlno = iSlno + 1
                            dr("SrNo") = iSlno
                            dr("CashRecieved") = ds.Tables(0).Rows(i)("PDBD_CashReceived").ToString()
                        Else
                            dr("SrNo") = ""
                            dr("CashRecieved") = ""
                        End If
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("pdbd_voucherno").ToString()) = False Then
                        dr("VoucherNo") = ds.Tables(0).Rows(i)("pdbd_voucherno").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("pdbd_debitamount").ToString()) = False Then
                        If ds.Tables(0).Rows(i)("pdbd_debitamount").ToString() <> 0.0000 Then
                            dr("Amount") = ds.Tables(0).Rows(i)("pdbd_debitamount").ToString()
                        Else

                            dr("Amount") = ""
                        End If
                    End If
                    If IsDBNull(ds.Tables(0).Rows(i)("pdbd_Narration").ToString()) = False Then
                        dr("Narration") = ds.Tables(0).Rows(i)("pdbd_Narration").ToString()
                    End If

                    dt.Rows.Add(dr)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadPereticulars(ByVal sNameSpace As String, ByVal iPerticulars As String) As String
        Dim sSql As String
        Dim perticular As String
        Try
            sSql = "Select  gl_glcode + '-' + gl_desc as GlDesc From chart_of_Accounts  Where  gl_id=" & iPerticulars & " "
            perticular = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            Return perticular
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SavePettyCashDayBookMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, objPCdb As ClsPettyCashDayBook)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(16) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PDB_PKID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objPCdb.iPdb_pkid
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PDB_TransactionNo", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objPCdb.sPdb_transactionNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PDB_ZoneID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objPCdb.iPDB_ZoneID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PDB_RegionID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objPCdb.iPDB_RegionID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PDB_AreaID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objPCdb.iPDB_AreaID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PDB_BranchID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objPCdb.iPDB_BranchID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PDB_CashTotal", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objPCdb.dPdb_cashTotal
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PDB_DebitTotal", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objPCdb.dPdb_DebitTotal
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PDB_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPCdb.iPDB_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PDB_Narration", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objPCdb.sPdb_Narration
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PDB_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPCdb.iPDB_YearId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PDB_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iCompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PDB_Status", OleDb.OleDbType.VarChar, 4)
            ObjParam(iParamCount).Value = objPCdb.sPDB_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1



            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PDB_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objPCdb.sPDB_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PDB_UpdatedBy", OleDb.OleDbType.Integer, 8)
            ObjParam(iParamCount).Value = objPCdb.iPDB_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "Usr_ACC_PettyCashDayBookMaster", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SavePettyCashDayBookDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, objPCdb As ClsPettyCashDayBook)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(15) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PDBDD_PKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPCdb.iPDBDD_PKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PDBD_MasterID", OleDb.OleDbType.Integer, 8)
            ObjParam(iParamCount).Value = objPCdb.iPDBD_MasterID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PDBD_CashReceived", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objPCdb.dPDBD_CashReceived
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PDBD_Date", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objPCdb.dPDBD_Date
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PDBD_Particulars", OleDb.OleDbType.VarChar)
            ObjParam(iParamCount).Value = objPCdb.sPDBD_Particulars
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PDBD_Voucherno", OleDb.OleDbType.VarChar)
            ObjParam(iParamCount).Value = objPCdb.sPDBD_Voucherno
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PDBD_DebitAmount", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objPCdb.dPDBD_DebitAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PDBD_Narration", OleDb.OleDbType.VarChar)
            ObjParam(iParamCount).Value = objPCdb.sPDBD_Narration
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PDBD_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPCdb.iPDBD_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PDBD_YearID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objPCdb.iPDBD_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PDBD_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iCompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PDBD_Status", OleDb.OleDbType.VarChar, 15)
            ObjParam(iParamCount).Value = objPCdb.sPDBD_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PDBD_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objPCdb.sPDBD_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@PDBD_UpdatedBy", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objPCdb.iPDBD_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "Usr_ACC_PettyCashDayBookMaster_Details", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveTransactionDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, objPCdb As ClsPettyCashDayBook)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(22) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPCdb.iATD_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_TransactionDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objPCdb.dATD_TransactionDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_TrType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPCdb.iATD_TrType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_BillID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPCdb.iATD_BillId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_PaymentType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPCdb.iATD_PaymentType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Head", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPCdb.iATD_Head
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_GL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPCdb.iATD_GL
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_SubGL", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPCdb.iATD_SubGL
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_DbOrCr", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPCdb.iATD_DbOrCr
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Debit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objPCdb.dATD_Debit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Credit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objPCdb.dATD_Credit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPCdb.iATD_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Status", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objPCdb.sATD_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPCdb.iATD_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iCompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_Operation", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objPCdb.sATD_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objPCdb.sATD_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_ZoneID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objPCdb.iATD_ZoneID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_RegionID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objPCdb.iATD_RegionID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_AreaID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objPCdb.iATD_AreaID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_BranchID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objPCdb.iATD_BranchID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_OpenDebit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objPCdb.dATD_OpenDebit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_OpenCredit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objPCdb.dATD_OpenCredit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_ClosingDebit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objPCdb.dATD_ClosingDebit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_ClosingCredit", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objPCdb.dATD_ClosingCredit
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ATD_SeqReferenceNum", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objPCdb.iATD_SeqReferenceNum
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spacc_Transactions_Details", 1, Arr, ObjParam)
            Return Arr
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
    Public Function LoadSubGLDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal igl As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select gl_id, gl_glcode + '-' + gl_desc as GlDesc from chart_of_accounts where "
            sSql = sSql & " gl_Parent=" & igl & " and gl_compid=" & iCompID & " and gl_status='A' and gl_Delflag ='C' order by gl_AccHead"
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
    Public Function LoadAssetSubGL(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAccHead As Integer) As Integer
        Dim sSql As String = ""
        Dim iAccGL As Integer
        Try
            sSql = "Select Acc_SubGl from acc_application_settings where Acc_Types='Cash' and Acc_CompID=" & iCompID & " "
            iAccGL = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return iAccGL
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAssetGL(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAccHead As Integer) As Integer
        Dim sSql As String = ""
        Dim iAccGL As Integer
        Try
            sSql = "Select Acc_Gl from acc_application_settings where Acc_Types='Cash' and Acc_CompID=" & iCompID & " "
            iAccGL = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return iAccGL
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetGLId(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearid As Integer, ByVal sPerticular As String) As Integer
        Dim sSql As String = ""
        Dim iGL As Integer
        Try
            sSql = "Select gl_parent from chart_of_accounts where gl_id=" & sPerticular & " and gl_CompID=" & iCompID & "  "
            iGL = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return iGL
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetHeadId(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearid As Integer, ByVal sPerticular As String) As Integer
        Dim sSql As String = ""
        Dim iG As Integer = 0
        Dim iHead As Integer = 0
        Try
            sSql = "Select gl_parent from chart_of_accounts where gl_id=" & sPerticular & " and gl_CompID=" & iCompID & "  "
            iG = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            If iG <> 0 Then
                sSql = "Select gl_Head from chart_of_accounts where gl_id=" & iG & " and gl_CompID=" & iCompID & "  "
                iHead = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            End If
            Return iHead
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function uploadStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearid As Integer, ByVal iPkId As Integer)
        Dim sSql As String = ""
        Try
            sSql = "update ACC_PettyCashDayBookMaster set pdb_status='A' where PDB_PKID=" & iPkId & " and PDB_compid=" & iCompID & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            sSql = "update ACC_PettyCashDayBookMaster_Details set pdbd_status='A' where PDBD_masterID=" & iPkId & " and PDBd_compid=" & iCompID & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function DeletePettyCashDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPKID As Integer, iMasterId As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Delete from ACC_PettyCashDayBookMaster_Details where pdbdd_pkid = " & iPKID & " and pdbd_CompID = " & iCompID & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            sSql = "Delete from acc_transactions_details where ATD_BillId = " & iMasterId & " and Atd_CompID = " & iCompID & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function uploadCreditAndDebit(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearid As Integer, ByVal iPkId As Integer, ByVal dCash As Double, dAmount As Double)
        Dim sSql As String = ""
        Try
            sSql = "update ACC_PettyCashDayBookMaster set pdb_CashTotal=" & dCash & " , pdb_DebitTotal=" & dAmount & " where PDB_PKID=" & iPkId & " and PDB_compid=" & iCompID & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function DeleteExePCDBTransDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPCID As Integer) As DataSet
        Dim sSql As String
        Try
            sSql = "Delete acc_Transactions_Details where ATD_BillId =" & iPCID & " And ATD_CompID =" & iCompID & " And ATD_TrType=12"
            Return objDBL.SQLExecuteDataSet(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

End Class
