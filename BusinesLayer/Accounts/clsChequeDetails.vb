Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class clsChequeDetails
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral


    Private iACMID As Integer
    Private sACMSerialNo As String
    Private iACMParty As Integer
    Private iACMBank As Integer
    Private iACMBranch As Integer
    Private sACMIFSCCode As String
    Private sACMPay As String
    Private sACMRupees As String
    Private mACMAmount As Double
    Private iACMChequeNo As Integer
    Private sACMChequeDate As String
    Private sACMAccountNo As String
    Private iACMAccountType As Integer
    Private iACMSalesPerson As Integer
    Private sACMRouteNo As String
    Private sACMCollectedDate As String
    Private sACMProducedDate As String
    Private sACMSummary As String
    Private sACMMICRCode As String
    Private iACMLeafNo As Integer
    Private iACMCompID As Integer
    Private iACMYearID As Integer
    Private sACMStatus As String
    Private iACMCreatedBy As Integer
    Private iACMUpdatedBy As Integer
    Private iACMDeletedBy As Integer
    Private iACMApprovedBy As Integer
    Private iACMAttachID As Integer
    Private sACMDelflag As String
    Private sACDContactNo As String
    Private sACDPANNo As String
    Private iACDID As Integer
    Private iACDMasterID As Integer
    Private iACDBankID As Integer
    Private sACDStatus As String
    Private iACDYearID As Integer
    Private iACDCompID As Integer
    Private iACDCreatedBy As Integer
    Public Property iACD_ID() As Integer
        Get
            Return (iACDID)
        End Get
        Set(ByVal Value As Integer)
            iACDID = Value
        End Set
    End Property
    Public Property iACD_CreatedBy() As Integer
        Get
            Return (iACDCreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iACDCreatedBy = Value
        End Set
    End Property
    Public Property iACD_CompID() As Integer
        Get
            Return (iACDCompID)
        End Get
        Set(ByVal Value As Integer)
            iACDCompID = Value
        End Set
    End Property
    Public Property iACD_YearID() As Integer
        Get
            Return (iACDYearID)
        End Get
        Set(ByVal Value As Integer)
            iACDYearID = Value
        End Set
    End Property
    Public Property sACD_Status() As String
        Get
            Return (sACDStatus)
        End Get
        Set(ByVal Value As String)
            sACDStatus = Value
        End Set
    End Property
    Public Property iACD_BankID() As Integer
        Get
            Return (iACDBankID)
        End Get
        Set(ByVal Value As Integer)
            iACDBankID = Value
        End Set
    End Property
    Public Property iACD_MasterID() As Integer
        Get
            Return (iACDMasterID)
        End Get
        Set(ByVal Value As Integer)
            iACDMasterID = Value
        End Set
    End Property
    Public Property sACD_ContactNo() As String
        Get
            Return (sACDContactNo)
        End Get
        Set(ByVal Value As String)
            sACDContactNo = Value
        End Set
    End Property
    Public Property sACD_PANNo() As String
        Get
            Return (sACDPANNo)
        End Get
        Set(ByVal Value As String)
            sACDPANNo = Value
        End Set
    End Property
    Public Property iACM_ID() As Integer
        Get
            Return (iACMID)
        End Get
        Set(ByVal Value As Integer)
            iACMID = Value
        End Set
    End Property
    Public Property sACM_SerialNo() As String
        Get
            Return (sACMSerialNo)
        End Get
        Set(ByVal Value As String)
            sACMSerialNo = Value
        End Set
    End Property
    Public Property iACM_Party() As Integer
        Get
            Return (iACMParty)
        End Get
        Set(ByVal Value As Integer)
            iACMParty = Value
        End Set
    End Property
    Public Property iACM_Bank() As Integer
        Get
            Return (iACMBank)
        End Get
        Set(ByVal Value As Integer)
            iACMBank = Value
        End Set
    End Property
    Public Property iACM_Branch() As Integer
        Get
            Return (iACMBranch)
        End Get
        Set(ByVal Value As Integer)
            iACMBranch = Value
        End Set
    End Property
    Public Property sACM_IFSCCode() As String
        Get
            Return (sACMIFSCCode)
        End Get
        Set(ByVal Value As String)
            sACMIFSCCode = Value
        End Set
    End Property
    Public Property sACM_Pay() As String
        Get
            Return (sACMPay)
        End Get
        Set(ByVal Value As String)
            sACMPay = Value
        End Set
    End Property
    Public Property sACM_Rupees() As String
        Get
            Return (sACMRupees)
        End Get
        Set(ByVal Value As String)
            sACMRupees = Value
        End Set
    End Property
    Public Property mACM_Amount() As Double
        Get
            Return (mACMAmount)
        End Get
        Set(ByVal Value As Double)
            mACMAmount = Value
        End Set
    End Property
    Public Property iACM_ChequeNo() As Integer
        Get
            Return (iACMChequeNo)
        End Get
        Set(ByVal Value As Integer)
            iACMChequeNo = Value
        End Set
    End Property
    Public Property sACM_ChequeDate() As String
        Get
            Return (sACMChequeDate)
        End Get
        Set(ByVal Value As String)
            sACMChequeDate = Value
        End Set
    End Property
    Public Property sACM_AccountNo() As String
        Get
            Return (sACMAccountNo)
        End Get
        Set(ByVal Value As String)
            sACMAccountNo = Value
        End Set
    End Property
    Public Property iACM_AccountType() As Integer
        Get
            Return (iACMAccountType)
        End Get
        Set(ByVal Value As Integer)
            iACMAccountType = Value
        End Set
    End Property
    Public Property iACM_SalesPerson() As Integer
        Get
            Return (iACMSalesPerson)
        End Get
        Set(ByVal Value As Integer)
            iACMSalesPerson = Value
        End Set
    End Property
    Public Property sACM_RouteNo() As String
        Get
            Return (sACMRouteNo)
        End Get
        Set(ByVal Value As String)
            sACMRouteNo = Value
        End Set
    End Property
    Public Property sACM_CollectedDate() As String
        Get
            Return (sACMCollectedDate)
        End Get
        Set(ByVal Value As String)
            sACMCollectedDate = Value
        End Set
    End Property
    Public Property sACM_ProducedDate() As String
        Get
            Return (sACMProducedDate)
        End Get
        Set(ByVal Value As String)
            sACMProducedDate = Value
        End Set
    End Property
    Public Property sACM_Summary() As String
        Get
            Return (sACMSummary)
        End Get
        Set(ByVal Value As String)
            sACMSummary = Value
        End Set
    End Property
    Public Property sACM_MICRCode() As String
        Get
            Return (sACMMICRCode)
        End Get
        Set(ByVal Value As String)
            sACMMICRCode = Value
        End Set
    End Property
    Public Property iACM_LeafNo() As Integer
        Get
            Return (iACMLeafNo)
        End Get
        Set(ByVal Value As Integer)
            iACMLeafNo = Value
        End Set
    End Property
    Public Property iACM_CompID() As Integer
        Get
            Return (iACMCompID)
        End Get
        Set(ByVal Value As Integer)
            iACMCompID = Value
        End Set
    End Property
    Public Property sACM_Status() As String
        Get
            Return (sACMStatus)
        End Get
        Set(ByVal Value As String)
            sACMStatus = Value
        End Set
    End Property
    Public Property iACM_YearID() As Integer
        Get
            Return (iACMYearID)
        End Get
        Set(ByVal Value As Integer)
            iACMYearID = Value
        End Set
    End Property
    Public Property iACM_UpdatedBy() As Integer
        Get
            Return (iACMUpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iACMUpdatedBy = Value
        End Set
    End Property
    Public Property iACM_CreatedBy() As Integer
        Get
            Return (iACMCreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iACMCreatedBy = Value
        End Set
    End Property
    Public Property iACM_DeletedBy() As Integer
        Get
            Return (iACMDeletedBy)
        End Get
        Set(ByVal Value As Integer)
            iACMDeletedBy = Value
        End Set
    End Property
    Public Property iACM_ApprovedBy() As Integer
        Get
            Return (iACMApprovedBy)
        End Get
        Set(ByVal Value As Integer)
            iACMApprovedBy = Value
        End Set
    End Property
    Public Property iACM_AttachID() As Integer
        Get
            Return (iACMAttachID)
        End Get
        Set(ByVal Value As Integer)
            iACMAttachID = Value
        End Set
    End Property
    Public Property sACM_Delflag() As String
        Get
            Return (sACMDelflag)
        End Get
        Set(ByVal Value As String)
            sACMDelflag = Value
        End Set
    End Property
    Public Function LoadPDCheDetails(ByVal sNameSpace As String, ByVal iACMID As Integer, ByVal iStatus As Integer, ByVal sDateToday As String) As DataTable
        Dim dt As New DataTable
        Dim ds As New DataSet
        Dim drow As DataRow
        Dim sSql As String = ""
        Dim dtAp As New DataTable

        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("ACM_ID")
            dt.Columns.Add("ACM_SerialNo")
            dt.Columns.Add("ACM_ChequeNo")
            dt.Columns.Add("ACM_ChequeDate")
            dt.Columns.Add("ACM_IFSCCode")
            dt.Columns.Add("ACM_AccountNo")
            dt.Columns.Add("ACM_Bank")
            dt.Columns.Add("ACM_Delflag")
            dt.Columns.Add("ACM_Amount")


            sSql = "Select * from Acc_Cheque_Masters "
            If iStatus = 0 Then
                sSql = sSql & " where ACM_Status ='A'"
            ElseIf iStatus = 1 Then
                sSql = sSql & " where ACM_Status ='A' and ACM_ChequeDate=" & sDateToday & ""
            ElseIf iStatus = 2 Then
                sSql = sSql & " where ACM_Status ='A' and ACM_ChequeDate > " & sDateToday & ""
            ElseIf iStatus = 3 Then
                sSql = sSql & " where ACM_Status ='A' and ACM_ChequeDate < " & sDateToday & ""
            End If
            dtAp = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dtAp.Rows.Count > 0 Then
                For i = 0 To dtAp.Rows.Count - 1
                    drow = dt.NewRow
                    drow("SrNo") = i + 1

                    If IsDBNull(dtAp.Rows(i)("ACM_ID")) = False Then
                        drow("ACM_ID") = dtAp.Rows(i)("ACM_ID")
                    End If
                    If IsDBNull(dtAp.Rows(i)("ACM_SerialNo")) = False Then
                        drow("ACM_SerialNo") = dtAp.Rows(i)("ACM_SerialNo")
                    End If
                    If IsDBNull(dtAp.Rows(i)("ACM_ChequeNo")) = False Then
                        drow("ACM_ChequeNo") = dtAp.Rows(i)("ACM_ChequeNo")
                    End If
                    If IsDBNull(dtAp.Rows(i)("ACM_ChequeDate")) = False Then
                        drow("ACM_ChequeDate") = dtAp.Rows(i)("ACM_ChequeDate")
                    End If
                    If IsDBNull(dtAp.Rows(i)("ACM_IFSCCode")) = False Then
                        drow("ACM_IFSCCode") = dtAp.Rows(i)("ACM_IFSCCode")
                    End If
                    If IsDBNull(dtAp.Rows(i)("ACM_AccountNo")) = False Then
                        drow("ACM_AccountNo") = dtAp.Rows(i)("ACM_AccountNo")
                    End If
                    If IsDBNull(dtAp.Rows(i)("ACM_Bank")) = False Then
                        ' drow("ACM_Bank") = dtAp.Rows(i)("ACM_Bank")
                        If dtAp.Rows(i)("ACM_Bank") = 1 Then
                            drow("ACM_Bank") = "SBI"
                        ElseIf dtAp.Rows(i)("ACM_Bank") = 2 Then
                            drow("ACM_Bank") = "Canara Bank"
                        ElseIf dtAp.Rows(i)("ACM_Bank") = 3 Then
                            drow("ACM_Bank") = "ICICI Bank"
                        ElseIf dtAp.Rows(i)("ACM_Bank") = 4 Then
                            drow("ACM_Bank") = "Axis Bank"
                        ElseIf dtAp.Rows(i)("ACM_Bank") = 5 Then
                            drow("ACM_Bank") = "Vijaya Bank"
                        ElseIf dtAp.Rows(i)("ACM_Bank") = 6 Then
                            drow("ACM_Bank") = "Indian Bank"
                        End If
                    End If
                    If IsDBNull(dtAp.Rows(i)("ACM_Delflag")) = False Then
                        'drow("ACM_Delflag") = dtAp.Rows(i)("ACM_Delflag")
                        If dtAp.Rows(i)("ACM_Delflag") = "N" Then
                            drow("ACM_Delflag") = "Not Prepared"
                        ElseIf dtAp.Rows(i)("ACM_Delflag") = "P" Then
                            drow("ACM_Delflag") = "Prepared"
                        End If
                    End If
                    If IsDBNull(dtAp.Rows(i)("ACM_Amount")) = False Then
                        drow("ACM_Amount") = dtAp.Rows(i)("ACM_Amount")
                    End If
                    dt.Rows.Add(drow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadExeChequeNo(ByVal sNameSpace As String, ByVal sSearch As String) As DataSet
        Dim sSql As String
        Try
            sSql = "Select ACM_ID,ACM_ChequeNo from Acc_Cheque_Masters where ACM_Status='A'"

            Return objDBL.SQLExecuteDataSet(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadExeChequeDetails(ByVal iACMID As Integer, ByVal sNameSpace As String) As DataTable
        Dim sSql As String
        Try
            sSql = "Select ACM_SerialNo,ACM_Party,ACM_ChequeNo,ACM_ChequeDate,ACM_IFSCCode,ACM_AccountNo,ACM_MICRCode,ACM_LeafNo,ACM_Bank,ACM_Branch,ACM_Pay,ACM_Rupees,ACM_Amount,ACM_AccountType,ACM_SalesPerson,ACM_RouteNo,ACM_CollectedDate,ACM_ProducedDate,ACM_Summary,ACM_AtachID from Acc_Cheque_Masters where ACM_ID=" & iACMID & ""
            Return objDBL.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function ChequeDetailsToExcel(ByVal sNameSpace As String) As DataTable
        Dim sSql As String
        Dim dtAp As New DataTable
        Dim drow As DataRow
        Dim dt As New DataTable
        Try

            dt.Columns.Add("ACM_ID")
            dt.Columns.Add("ACM_SerialNo")
            dt.Columns.Add("ACM_Party")
            dt.Columns.Add("ACM_ChequeNo")
            dt.Columns.Add("ACM_ChequeDate")
            dt.Columns.Add("ACM_IFSCCode")
            dt.Columns.Add("ACM_AccountNo")
            dt.Columns.Add("ACM_MICRCode")
            dt.Columns.Add("ACM_LeafNo")
            dt.Columns.Add("ACM_Bank")
            dt.Columns.Add("ACM_Branch")
            dt.Columns.Add("ACM_Pay")
            dt.Columns.Add("ACM_Rupees")
            dt.Columns.Add("ACM_Amount")
            dt.Columns.Add("ACM_AccountType")
            dt.Columns.Add("ACM_SalesPerson")
            dt.Columns.Add("ACM_RouteNo")
            dt.Columns.Add("ACM_CollectedDate")
            dt.Columns.Add("ACM_ProducedDate")
            dt.Columns.Add("ACM_Summary")

            sSql = "Select ACM_ID,ACM_SerialNo,ACM_Party,ACM_ChequeNo,ACM_ChequeDate,ACM_IFSCCode,ACM_AccountNo,ACM_MICRCode,ACM_LeafNo,ACM_Bank,"
            sSql = sSql & " ACM_Branch,ACM_Pay,ACM_Rupees,ACM_Amount,ACM_AccountType,ACM_SalesPerson,ACM_RouteNo,ACM_CollectedDate,ACM_ProducedDate,"
            sSql = sSql & " ACM_Summary from Acc_Cheque_Masters where ACM_Status ='A' "

            dtAp = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dtAp.Rows.Count > 0 Then
                For i = 0 To dtAp.Rows.Count - 1
                    drow = dt.NewRow
                    If IsDBNull(dtAp.Rows(i)("ACM_ID")) = False Then
                        drow("ACM_ID") = dtAp.Rows(i)("ACM_ID")
                    End If
                    If IsDBNull(dtAp.Rows(i)("ACM_SerialNo")) = False Then
                        drow("ACM_SerialNo") = dtAp.Rows(i)("ACM_SerialNo")
                    End If
                    If IsDBNull(dtAp.Rows(i)("ACM_Party")) = False Then
                        drow("ACM_Party") = dtAp.Rows(i)("ACM_Party")
                    End If
                    If IsDBNull(dtAp.Rows(i)("ACM_ChequeNo")) = False Then
                        drow("ACM_ChequeNo") = dtAp.Rows(i)("ACM_ChequeNo")
                    End If
                    If IsDBNull(dtAp.Rows(i)("ACM_ChequeDate")) = False Then
                        drow("ACM_ChequeDate") = dtAp.Rows(i)("ACM_ChequeDate")
                    End If
                    If IsDBNull(dtAp.Rows(i)("ACM_IFSCCode")) = False Then
                        drow("ACM_IFSCCode") = dtAp.Rows(i)("ACM_IFSCCode")
                    End If
                    If IsDBNull(dtAp.Rows(i)("ACM_AccountNo")) = False Then
                        drow("ACM_AccountNo") = dtAp.Rows(i)("ACM_AccountNo")
                    End If
                    If IsDBNull(dtAp.Rows(i)("ACM_MICRCode")) = False Then
                        drow("ACM_MICRCode") = dtAp.Rows(i)("ACM_MICRCode")
                    End If
                    If IsDBNull(dtAp.Rows(i)("ACM_LeafNo")) = False Then
                        drow("ACM_LeafNo") = dtAp.Rows(i)("ACM_LeafNo")
                    End If
                    If IsDBNull(dtAp.Rows(i)("ACM_Bank")) = False Then

                        If dtAp.Rows(i)("ACM_Bank") = 1 Then
                            drow("ACM_Bank") = "SBI"
                        ElseIf dtAp.Rows(i)("ACM_Bank") = 2 Then
                            drow("ACM_Bank") = "Canara Bank"
                        ElseIf dtAp.Rows(i)("ACM_Bank") = 3 Then
                            drow("ACM_Bank") = "ICICI Bank"
                        ElseIf dtAp.Rows(i)("ACM_Bank") = 4 Then
                            drow("ACM_Bank") = "Axis Bank"
                        ElseIf dtAp.Rows(i)("ACM_Bank") = 5 Then
                            drow("ACM_Bank") = "Vijaya Bank"
                        ElseIf dtAp.Rows(i)("ACM_Bank") = 6 Then
                            drow("ACM_Bank") = "Indian Bank"
                        End If
                    End If
                    If IsDBNull(dtAp.Rows(i)("ACM_Branch")) = False Then
                        If dtAp.Rows(i)("ACM_Branch") = 1 Then
                            drow("ACM_Branch") = "Branch 1"
                        ElseIf dtAp.Rows(i)("ACM_Branch") = 2 Then
                            drow("ACM_Branch") = "Branch 2"
                        ElseIf dtAp.Rows(i)("ACM_Branch") = 3 Then
                            drow("ACM_Branch") = "Branch 3"
                        ElseIf dtAp.Rows(i)("ACM_Branch") = 4 Then
                            drow("ACM_Branch") = "Branch 4"
                        ElseIf dtAp.Rows(i)("ACM_Branch") = 5 Then
                            drow("ACM_Branch") = "Branch 5"
                        ElseIf dtAp.Rows(i)("ACM_Branch") = 6 Then
                            drow("ACM_Branch") = "Branch 6"
                        End If
                    End If
                    If IsDBNull(dtAp.Rows(i)("ACM_Pay")) = False Then
                        drow("ACM_Pay") = dtAp.Rows(i)("ACM_Pay")
                    End If
                    If IsDBNull(dtAp.Rows(i)("ACM_Rupees")) = False Then
                        drow("ACM_Rupees") = dtAp.Rows(i)("ACM_Rupees")
                    End If
                    If IsDBNull(dtAp.Rows(i)("ACM_Amount")) = False Then
                        drow("ACM_Amount") = dtAp.Rows(i)("ACM_Amount")
                    End If
                    If IsDBNull(dtAp.Rows(i)("ACM_AccountType")) = False Then
                        If dtAp.Rows(i)("ACM_AccountType") = 1 Then
                            drow("ACM_AccountType") = "savings Account"
                        ElseIf dtAp.Rows(i)("ACM_AccountType") = 2 Then
                            drow("ACM_AccountType") = "Current Account"
                        ElseIf dtAp.Rows(i)("ACM_AccountType") = 3 Then
                            drow("ACM_AccountType") = "Others"
                        End If
                    End If
                    If IsDBNull(dtAp.Rows(i)("ACM_SalesPerson")) = False Then
                        If dtAp.Rows(i)("ACM_SalesPerson") = 1 Then
                            drow("ACM_SalesPerson") = "SP 1"
                        ElseIf dtAp.Rows(i)("ACM_SalesPerson") = 2 Then
                            drow("ACM_SalesPerson") = "SP 2"
                        ElseIf dtAp.Rows(i)("ACM_SalesPerson") = 3 Then
                            drow("ACM_SalesPerson") = "SP 3"
                        ElseIf dtAp.Rows(i)("ACM_SalesPerson") = 4 Then
                            drow("ACM_SalesPerson") = "SP 4"
                        ElseIf dtAp.Rows(i)("ACM_SalesPerson") = 5 Then
                            drow("ACM_SalesPerson") = "SP 5"
                        ElseIf dtAp.Rows(i)("ACM_SalesPerson") = 6 Then
                            drow("ACM_SalesPerson") = "SP 6"
                        End If
                    End If
                    If IsDBNull(dtAp.Rows(i)("ACM_RouteNo")) = False Then
                        drow("ACM_RouteNo") = dtAp.Rows(i)("ACM_RouteNo")
                    End If
                    If IsDBNull(dtAp.Rows(i)("ACM_CollectedDate")) = False Then
                        drow("ACM_CollectedDate") = dtAp.Rows(i)("ACM_CollectedDate")
                    End If
                    If IsDBNull(dtAp.Rows(i)("ACM_ProducedDate")) = False Then
                        drow("ACM_ProducedDate") = dtAp.Rows(i)("ACM_ProducedDate")
                    End If
                    If IsDBNull(dtAp.Rows(i)("ACM_Summary")) = False Then
                        drow("ACM_Summary") = dtAp.Rows(i)("ACM_Summary")
                    End If
                    dt.Rows.Add(drow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadChallanDetails(ByVal sNameSpace As String, ByVal iStatus As Integer, ByVal sDate As String, ByVal iMasID As Integer) As DataTable
        Dim sSql As String
        Dim dtAp As New DataTable
        Dim drow As DataRow
        Dim dt As New DataTable
        Try

            dt.Columns.Add("ACM_ID")
            dt.Columns.Add("ACM_ChequeNo")
            dt.Columns.Add("ACM_AccountNo")
            dt.Columns.Add("ACM_ChequeDate")
            dt.Columns.Add("ACM_Pay")
            dt.Columns.Add("ACM_Amount")
            dt.Columns.Add("ACD_ContactNo")
            dt.Columns.Add("ACD_PANNo")


            sSql = "Select * from Acc_Cheque_Masters where ACM_Status ='A' and ACM_Delflag='N'and ACM_ChequeDate='" & sDate & "'"
            If iStatus = 1 Then
                sSql = sSql & " and ACM_Bank = 1"
            ElseIf iStatus = 2 Then
                sSql = sSql & " and ACM_Bank = 2"
            ElseIf iStatus = 3 Then
                sSql = sSql & " and ACM_Bank = 3"
            ElseIf iStatus = 4 Then
                sSql = sSql & " and ACM_Bank = 4 "
            ElseIf iStatus = 5 Then
                sSql = sSql & " and ACM_Bank = 5 "
            ElseIf iStatus = 6 Then
                sSql = sSql & " and ACM_Bank = 6 "
            End If

            dtAp = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dtAp.Rows.Count > 0 Then
                For i = 0 To dtAp.Rows.Count - 1
                    drow = dt.NewRow
                    If IsDBNull(dtAp.Rows(i)("ACM_ID")) = False Then
                        drow("ACM_ID") = dtAp.Rows(i)("ACM_ID")
                    End If
                    If IsDBNull(dtAp.Rows(i)("ACM_ChequeNo")) = False Then
                        drow("ACM_ChequeNo") = dtAp.Rows(i)("ACM_ChequeNo")
                    End If
                    If IsDBNull(dtAp.Rows(i)("ACM_AccountNo")) = False Then
                        drow("ACM_AccountNo") = dtAp.Rows(i)("ACM_AccountNo")
                    End If
                    If IsDBNull(dtAp.Rows(i)("ACM_ChequeDate")) = False Then
                        drow("ACM_ChequeDate") = dtAp.Rows(i)("ACM_ChequeDate")
                    End If
                    If IsDBNull(dtAp.Rows(i)("ACM_Pay")) = False Then
                        drow("ACM_Pay") = dtAp.Rows(i)("ACM_Pay")
                    End If
                    If IsDBNull(dtAp.Rows(i)("ACM_Amount")) = False Then
                        drow("ACM_Amount") = dtAp.Rows(i)("ACM_Amount")
                    End If
                    drow("ACD_ContactNo") = ""
                    drow("ACD_PANNo") = ""
                    dt.Rows.Add(drow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetMaxID(ByVal sNameSpace As String, ByVal iACMID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select Count(ACM_ID)+1 from Acc_Cheque_Masters "
            Return objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub DelChkDetails(ByVal sNameSpace As String, ByVal iACMdelBy As Integer, ByVal iACMID As Integer)
        Dim sSql As String
        Try
            sSql = "Update Acc_Cheque_Masters set ACM_Status='D', ACM_DeletedBy=" & iACMdelBy & ", ACM_DeletedOn=getdate() where ACM_Id=" & iACMID & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function SaveChequeDetails(ByVal objCD As clsChequeDetails, ByVal sNameSpace As String) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(29) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCD.iACM_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_SerialNo", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objCD.sACM_SerialNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_Party", OleDb.OleDbType.Integer, 10)
            ObjParam(iParamCount).Value = objCD.iACM_Party
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_Bank", OleDb.OleDbType.Integer, 10)
            ObjParam(iParamCount).Value = objCD.iACM_Bank
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_Branch", OleDb.OleDbType.Integer, 10)
            ObjParam(iParamCount).Value = objCD.iACM_Branch
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_IFSCCode", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objCD.sACM_IFSCCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_Pay", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objCD.sACM_Pay
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_Rupees", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objCD.sACM_Rupees
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_Amount", OleDb.OleDbType.Double, 100)
            ObjParam(iParamCount).Value = objCD.mACM_Amount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_ChequeNo", OleDb.OleDbType.Integer, 6)
            ObjParam(iParamCount).Value = objCD.iACM_ChequeNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_ChequeDate", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objCD.sACM_ChequeDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_AccountNo", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objCD.sACM_AccountNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_AccountType", OleDb.OleDbType.Integer, 10)
            ObjParam(iParamCount).Value = objCD.iACM_AccountType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_SalesPerson", OleDb.OleDbType.Integer, 10)
            ObjParam(iParamCount).Value = objCD.iACM_SalesPerson
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_RouteNo", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objCD.sACM_RouteNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_CollectedDate", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objCD.sACM_CollectedDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_ProducedDate", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objCD.sACM_ProducedDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_Summary", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objCD.sACM_Summary
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_MICRCode", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objCD.sACM_MICRCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_LeafNo", OleDb.OleDbType.Integer, 100)
            ObjParam(iParamCount).Value = objCD.iACM_LeafNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_CompID", OleDb.OleDbType.Integer, 10)
            ObjParam(iParamCount).Value = objCD.iACM_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_YearID", OleDb.OleDbType.Integer, 10)
            ObjParam(iParamCount).Value = objCD.iACM_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_Status", OleDb.OleDbType.VarChar, 5)
            ObjParam(iParamCount).Value = objCD.sACM_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_CreatedBy", OleDb.OleDbType.Integer, 10)
            ObjParam(iParamCount).Value = objCD.iACM_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_UpdatedBy", OleDb.OleDbType.Integer, 10)
            ObjParam(iParamCount).Value = objCD.iACM_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iACM_ApprovedBy", OleDb.OleDbType.Integer, 10)
            ObjParam(iParamCount).Value = objCD.iACM_ApprovedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_AtachId", OleDb.OleDbType.Integer, 10)
            ObjParam(iParamCount).Value = objCD.iACM_AttachID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_Delflag", OleDb.OleDbType.VarChar, 5)
            ObjParam(iParamCount).Value = objCD.sACM_Delflag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spAcc_Cheque_Masters", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveChallanDetails(ByVal objCD As clsChequeDetails, ByVal sNameSpace As String) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(10) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACD_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCD.iACD_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACD_MasterID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCD.iACD_MasterID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACD_BankID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCD.iACD_BankID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACD_ContactNo", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objCD.sACD_ContactNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACD_PANNo", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objCD.sACD_PANNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACD_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objCD.sACD_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACD_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCD.iACD_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACD_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCD.iACD_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACD_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCD.iACD_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spAcc_Challen_Details", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateDelflag(ByVal sNameSpace As String, ByVal iACMID As Integer)
        Dim sSql As String
        Try
            sSql = "Update Acc_Cheque_Masters set ACM_Delflag='P' where ACM_Id=" & iACMID & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class
