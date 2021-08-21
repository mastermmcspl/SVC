Imports System
Imports DatabaseLayer
Imports System.Data
Public Class clsEmployeeMaster
    Private objDBL As New DatabaseLayer.DBHelper
    Private objclsGRACeGeneral As New clsFASGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private Usr_ID As Integer
    Private Usr_Node As Integer
    Private Usr_Code As String
    Private Usr_FullName As String
    Private Usr_LoginName As String
    Private Usr_Password As String
    Private Usr_Email As String
    Private Usr_LevelGrp As Integer
    Private Usr_DutyStatus As String
    Private Usr_PhoneNo As String
    Private Usr_MobileNo As String
    Private Usr_OfficePhone As String
    Private Usr_OffPhExtn As String
    Private Usr_Designation As Integer
    Private Usr_CompanyID As Integer
    Private Usr_OrgID As Integer
    Private Usr_GrpOrUserLvlPerm As Integer
    Private Usr_NoOfUnsucsfAtteptts As Integer
    Private Usr_Ques As String
    Private Usr_Ans As String
    Private Usr_SentMail As Integer
    Private Usr_Partner As Integer
    Private Usr_NoOfLogin As Integer
    Private Usr_LastLoginDate As Date
    Private Usr_CreatedBy As Integer
    Private Usr_CreatedOn As Date
    Private Usr_UpdatedBy As Integer
    Private Usr_UpdatedOn As Date
    Private Usr_AppBy As Integer
    Private Usr_AppOn As Date
    Private Usr_DeletedBy As Integer
    Private Usr_DeletedOn As Date
    Private Usr_RecallBy As Integer
    Private Usr_RecallOn As Date
    Private Usr_Flag As String
    Private Usr_Status As String
    Private Usr_CompId As Integer
    Private Usr_Role As Integer
    Private Usr_MasterModule As Integer
    Private Usr_PurchaseModule As Integer
    Private Usr_SalesModule As Integer
    Private Usr_AccountsModule As Integer
    Private Usr_MasterRole As Integer
    Private Usr_PurchaseRole As Integer
    Private Usr_SalesRole As Integer
    Private Usr_AccountsRole As Integer
    Private Usr_IPAdress As String
    Public Property iUsrPurchaseModule() As Integer
        Get
            Return (Usr_PurchaseModule)
        End Get
        Set(ByVal Value As Integer)
            Usr_PurchaseModule = Value
        End Set
    End Property
    Public Property iUsrPurchaseRole() As Integer
        Get
            Return (Usr_PurchaseRole)
        End Get
        Set(ByVal Value As Integer)
            Usr_PurchaseRole = Value
        End Set
    End Property
    Public Property iUsrSalesRole() As Integer
        Get
            Return (Usr_SalesRole)
        End Get
        Set(ByVal Value As Integer)
            Usr_SalesRole = Value
        End Set
    End Property
    Public Property iUsrSalesModule() As Integer
        Get
            Return (Usr_SalesModule)
        End Get
        Set(ByVal Value As Integer)
            Usr_SalesModule = Value
        End Set
    End Property
    Public Property iUsrAccountsRole() As Integer
        Get
            Return (Usr_AccountsRole)
        End Get
        Set(ByVal Value As Integer)
            Usr_AccountsRole = Value
        End Set
    End Property

    Public Property iUsrAccountsModule() As Integer
        Get
            Return (Usr_AccountsModule)
        End Get
        Set(ByVal Value As Integer)
            Usr_AccountsModule = Value
        End Set
    End Property

    Public Property iUsrMasterModule() As Integer
        Get
            Return (Usr_MasterModule)
        End Get
        Set(ByVal Value As Integer)
            Usr_MasterModule = Value
        End Set
    End Property
    Public Property iUsrMasterRole() As Integer
        Get
            Return (Usr_MasterRole)
        End Get
        Set(ByVal Value As Integer)
            Usr_MasterRole = Value
        End Set
    End Property
    Public Property iUsrRole() As Integer
        Get
            Return (Usr_Role)
        End Get
        Set(ByVal Value As Integer)
            Usr_Role = Value
        End Set
    End Property
    Public Property iUsrCompID() As Integer
        Get
            Return (Usr_CompId)
        End Get
        Set(ByVal Value As Integer)
            Usr_CompId = Value
        End Set
    End Property
    Public Property sUsrStatus() As String
        Get
            Return (Usr_Status)
        End Get
        Set(ByVal Value As String)
            Usr_Status = Value
        End Set
    End Property
    Public Property sUsrFlag() As String
        Get
            Return (Usr_Flag)
        End Get
        Set(ByVal Value As String)
            Usr_Flag = Value
        End Set
    End Property
    Public Property dUsrRecallOn() As Date
        Get
            Return (Usr_RecallOn)
        End Get
        Set(ByVal Value As Date)
            Usr_RecallOn = Value
        End Set
    End Property
    Public Property iUsrRecallBy() As Integer
        Get
            Return (Usr_RecallBy)
        End Get
        Set(ByVal Value As Integer)
            Usr_RecallBy = Value
        End Set
    End Property
    Public Property dUsrDeletedOn() As Date
        Get
            Return (Usr_DeletedOn)
        End Get
        Set(ByVal Value As Date)
            Usr_DeletedOn = Value
        End Set
    End Property
    Public Property iUsrDeletedBy() As Integer
        Get
            Return (Usr_DeletedBy)
        End Get
        Set(ByVal Value As Integer)
            Usr_DeletedBy = Value
        End Set
    End Property
    Public Property dUsrAppOn() As Date
        Get
            Return (Usr_AppOn)
        End Get
        Set(ByVal Value As Date)
            Usr_AppOn = Value
        End Set
    End Property
    Public Property iUsrAppBy() As Integer
        Get
            Return (Usr_AppBy)
        End Get
        Set(ByVal Value As Integer)
            Usr_AppBy = Value
        End Set
    End Property
    Public Property dUsrUpdatedOn() As Date
        Get
            Return (Usr_UpdatedOn)
        End Get
        Set(ByVal Value As Date)
            Usr_UpdatedOn = Value
        End Set
    End Property
    Public Property iUsrUpdatedBy() As Integer
        Get
            Return (Usr_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            Usr_UpdatedBy = Value
        End Set
    End Property
    Public Property dUsrCreatedOn() As Date
        Get
            Return (Usr_CreatedOn)
        End Get
        Set(ByVal Value As Date)
            Usr_CreatedOn = Value
        End Set
    End Property
    Public Property iUsrCreatedBy() As Integer
        Get
            Return (Usr_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            Usr_CreatedBy = Value
        End Set
    End Property
    Public Property dUsrLastLoginDate() As Date
        Get
            Return (Usr_LastLoginDate)
        End Get
        Set(ByVal Value As Date)
            Usr_LastLoginDate = Value
        End Set
    End Property
    Public Property iUsrNoOfLogin() As Integer
        Get
            Return (Usr_NoOfLogin)
        End Get
        Set(ByVal Value As Integer)
            Usr_NoOfLogin = Value
        End Set
    End Property
    Public Property iUsrPartner() As Integer
        Get
            Return (Usr_Partner)
        End Get
        Set(ByVal Value As Integer)
            Usr_Partner = Value
        End Set
    End Property
    Public Property iUsrSentMail() As Integer
        Get
            Return (Usr_SentMail)
        End Get
        Set(ByVal Value As Integer)
            Usr_SentMail = Value
        End Set
    End Property
    Public Property sUsrAns() As String
        Get
            Return (Usr_Ans)
        End Get
        Set(ByVal Value As String)
            Usr_Ans = Value
        End Set
    End Property
    Public Property sUsrQues() As String
        Get
            Return (Usr_Ques)
        End Get
        Set(ByVal Value As String)
            Usr_Ques = Value
        End Set
    End Property
    Public Property iUsrNoOfUnsucsfAtteptts() As Integer
        Get
            Return (Usr_NoOfUnsucsfAtteptts)
        End Get
        Set(ByVal Value As Integer)
            Usr_NoOfUnsucsfAtteptts = Value
        End Set
    End Property
    Public Property iUsrGrpOrUserLvlPerm() As Integer
        Get
            Return (Usr_GrpOrUserLvlPerm)
        End Get
        Set(ByVal Value As Integer)
            Usr_GrpOrUserLvlPerm = Value
        End Set
    End Property
    Public Property iUsrOrgID() As Integer
        Get
            Return (Usr_OrgID)
        End Get
        Set(ByVal Value As Integer)
            Usr_OrgID = Value
        End Set
    End Property
    Public Property iUsrCompanyID() As Integer
        Get
            Return (Usr_CompanyID)
        End Get
        Set(ByVal Value As Integer)
            Usr_CompanyID = Value
        End Set
    End Property
    Public Property iUsrDesignation() As Integer
        Get
            Return (Usr_Designation)
        End Get
        Set(ByVal Value As Integer)
            Usr_Designation = Value
        End Set
    End Property
    Public Property sUsrOffPhExtn() As String
        Get
            Return (Usr_OffPhExtn)
        End Get
        Set(ByVal Value As String)
            Usr_OffPhExtn = Value
        End Set
    End Property
    Public Property sUsrOfficePhone() As String
        Get
            Return (Usr_OfficePhone)
        End Get
        Set(ByVal Value As String)
            Usr_OfficePhone = Value
        End Set
    End Property
    Public Property sUsrPhoneNo() As String
        Get
            Return (Usr_PhoneNo)
        End Get
        Set(ByVal Value As String)
            Usr_PhoneNo = Value
        End Set
    End Property
    Public Property sUsrMobileNo() As String
        Get
            Return (Usr_MobileNo)
        End Get
        Set(ByVal Value As String)
            Usr_MobileNo = Value
        End Set
    End Property
    Public Property sUsrDutyStatus() As String
        Get
            Return (Usr_DutyStatus)
        End Get
        Set(ByVal Value As String)
            Usr_DutyStatus = Value
        End Set
    End Property
    Public Property iUsrLevelGrp() As Integer
        Get
            Return (Usr_LevelGrp)
        End Get
        Set(ByVal Value As Integer)
            Usr_LevelGrp = Value
        End Set
    End Property
    Public Property sUsrEmail() As String
        Get
            Return (Usr_Email)
        End Get
        Set(ByVal Value As String)
            Usr_Email = Value
        End Set
    End Property
    Public Property sUsrPassword() As String
        Get
            Return (Usr_Password)
        End Get
        Set(ByVal Value As String)
            Usr_Password = Value
        End Set
    End Property
    Public Property sUsrLoginName() As String
        Get
            Return (Usr_LoginName)
        End Get
        Set(ByVal Value As String)
            Usr_LoginName = Value
        End Set
    End Property
    Public Property sUsrFullName() As String
        Get
            Return (Usr_FullName)
        End Get
        Set(ByVal Value As String)
            Usr_FullName = Value
        End Set
    End Property
    Public Property sUsrCode() As String
        Get
            Return (Usr_Code)
        End Get
        Set(ByVal Value As String)
            Usr_Code = Value
        End Set
    End Property
    Public Property iUsrNode() As Integer
        Get
            Return (Usr_Node)
        End Get
        Set(ByVal Value As Integer)
            Usr_Node = Value
        End Set
    End Property
    Public Property iUserID() As Integer
        Get
            Return (Usr_ID)
        End Get
        Set(ByVal Value As Integer)
            Usr_ID = Value
        End Set
    End Property
    Public Property sUsrIPAdress() As String
        Get
            Return (Usr_IPAdress)
        End Get
        Set(ByVal Value As String)
            Usr_IPAdress = Value
        End Set
    End Property
    Public Function LoadAllEmpDetails(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim dt As New DataTable, dtZoneRegionBranchAreaDetails As New DataTable, dtDetails As New DataTable
        Dim dRow As DataRow
        Dim sSql As String, sModuleRole As String = ""
        Try
            dtZoneRegionBranchAreaDetails = GetZoneRegionAreaBranch(sAC, iACID)

            dt.Columns.Add("SrNo")
            dt.Columns.Add("SAPCode")
            dt.Columns.Add("EmpID")
            dt.Columns.Add("EmployeeName")
            dt.Columns.Add("LoginName")
            dt.Columns.Add("Designation")
            dt.Columns.Add("Module")
            dt.Columns.Add("LastLogin")
            dt.Columns.Add("Zone")
            dt.Columns.Add("Region")
            dt.Columns.Add("Area")
            dt.Columns.Add("Branch")
            dt.Columns.Add("Status")

            sSql = "Select a.usr_id,a.usr_node,(a.Usr_FullName + ' - ' + a.Usr_Code) as FullName,a.Usr_Role,a.usr_FullName,a.Usr_LoginName,a.usr_Code,a.usr_DutyStatus,"
            sSql = sSql & " a.usr_Node,a.Usr_OrgnID,a.usr_LevelGrp,a.usr_GrpOrUserLvlPerm,"
            sSql = sSql & " a.Usr_MasterModule,a.Usr_MasterRole,a.Usr_PurchaseModule,a.usr_PurchaseRole,a.Usr_SalesModule,a.usr_SalesRole,a.usr_delFlag,"
            sSql = sSql & " a.Usr_AccountsModule,a.usr_AccountsRole,"
            sSql = sSql & " a.usr_DelFlag,a.USR_LastLoginDate,b.mas_Desc as Designation,d.mas_Description As MasterRole,e.mas_Description As PurchaseRole,"
            sSql = sSql & " f.mas_Description As SalesRole,g.mas_Description As AccountsRole, a.USR_LastLoginDate from sad_userdetails a "
            sSql = sSql & " Left Join ACC_General_Master b On a.usr_Designation=b.mas_ID "
            sSql = sSql & " left join SAD_GrpOrLvl_General_Master d On a.Usr_MasterRole=d.mas_ID"
            sSql = sSql & " left join SAD_GrpOrLvl_General_Master e On a.usr_PurchaseRole=e.mas_ID"
            sSql = sSql & " left join SAD_GrpOrLvl_General_Master f On a.usr_SalesRole=f.mas_ID"
            sSql = sSql & " left join SAD_GrpOrLvl_General_Master g On a.usr_AccountsRole=g.mas_ID"
            sSql = sSql & " where Usr_CompID=" & iACID & " And Usr_Node>0 And Usr_OrgnID>0 "
            sSql = sSql & " order by FullName"
            dtDetails = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)

            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                dRow("SrNo") = i + 1
                If IsDBNull(dtDetails.Rows(i)("usr_Code")) = False Then
                    dRow("SAPCode") = dtDetails.Rows(i)("usr_Code")
                End If
                If IsDBNull(dtDetails.Rows(i)("usr_Code")) = False Then
                    dRow("EmpID") = dtDetails.Rows(i)("usr_id")
                End If
                If IsDBNull(dtDetails.Rows(i)("usr_FullName")) = False Then
                    dRow("EmployeeName") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("usr_FullName"))
                End If
                If IsDBNull(dtDetails.Rows(i)("Usr_LoginName")) = False Then
                    dRow("LoginName") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("Usr_LoginName"))
                End If
                If IsDBNull(dtDetails.Rows(i)("Designation")) = False Then
                    dRow("Designation") = dtDetails.Rows(i)("Designation")
                End If

                dRow("Zone") = ""
                dRow("Region") = ""
                dRow("Area") = ""
                dRow("Branch") = ""
                If IsDBNull(dtDetails.Rows(i)("usr_Node")) = False And IsDBNull(dtDetails.Rows(i)("usr_orgnid")) = False Then
                    Dim dtGDDeatils As New DataTable, dtGD As New DataTable
                    Dim iAreaID As Integer, iRegionID As Integer, iZoneID As Integer

                    Dim DVZRBADetails As New DataView(dtZoneRegionBranchAreaDetails)
                    DVZRBADetails.RowFilter = "Org_Node=" & dtDetails.Rows(i)("usr_orgnid") & " And Org_levelCode=" & dtDetails.Rows(i)("usr_node") & ""
                    dtGDDeatils = DVZRBADetails.ToTable

                    If dtGDDeatils.Rows.Count > 0 Then
                        If dtDetails.Rows(i)("usr_Node") = 4 Then
                            dRow("Branch") = dtGDDeatils.Rows(0)("Org_Name")
                            iAreaID = dtGDDeatils.Rows(0)("Org_Parent")
                            dtGD = Nothing
                            DVZRBADetails.RowFilter = "Org_Node=" & iAreaID & " And Org_levelCode=3"
                            dtGD = DVZRBADetails.ToTable
                            If dtGD.Rows.Count > 0 Then
                                dRow("Area") = dtGD.Rows(0)("Org_Name")
                                iRegionID = dtGD.Rows(0)("Org_Parent")
                                dtGD = Nothing
                                DVZRBADetails.RowFilter = "Org_Node=" & iRegionID & " And Org_levelCode=2"
                                dtGD = DVZRBADetails.ToTable
                                If dtGD.Rows.Count > 0 Then
                                    dRow("Region") = dtGD.Rows(0)("Org_Name")
                                    iZoneID = dtGD.Rows(0)("Org_Parent")
                                    dtGD = Nothing
                                    DVZRBADetails.RowFilter = "Org_Node=" & iZoneID & " And Org_levelCode=1"
                                    dtGD = DVZRBADetails.ToTable
                                    If dtGD.Rows.Count > 0 Then
                                        dRow("Zone") = dtGD.Rows(0)("Org_Name")
                                    End If
                                End If
                            End If
                        End If

                        If dtDetails.Rows(i)("usr_Node") = 3 Then
                            If dtGDDeatils.Rows.Count > 0 Then
                                dRow("Area") = dtGDDeatils.Rows(0)("Org_Name")
                                iRegionID = dtGDDeatils.Rows(0)("Org_Parent")
                                dtGD = Nothing
                                DVZRBADetails.RowFilter = "Org_Node=" & iRegionID & " And Org_levelCode=2"
                                dtGD = DVZRBADetails.ToTable
                                If dtGD.Rows.Count > 0 Then
                                    dRow("Region") = dtGD.Rows(0)("Org_Name")
                                    iZoneID = dtGD.Rows(0)("Org_Parent")
                                    dtGD = Nothing
                                    DVZRBADetails.RowFilter = "Org_Node=" & iZoneID & " And Org_levelCode=1"
                                    dtGD = DVZRBADetails.ToTable
                                    If dtGD.Rows.Count > 0 Then
                                        dRow("Zone") = dtGD.Rows(0)("Org_Name")
                                    End If
                                End If
                            End If
                        End If

                        If dtDetails.Rows(i)("usr_Node") = 2 Then
                            If dtGDDeatils.Rows.Count > 0 Then
                                dRow("Region") = dtGDDeatils.Rows(0)("Org_Name")
                                iZoneID = dtGDDeatils.Rows(0)("Org_Parent")
                                dtGD = Nothing
                                DVZRBADetails.RowFilter = "Org_Node=" & iZoneID & " And Org_levelCode=1"
                                dtGD = DVZRBADetails.ToTable
                                If dtGD.Rows.Count > 0 Then
                                    dRow("Zone") = dtGD.Rows(0)("Org_Name")
                                End If
                            End If
                        End If

                        If dtDetails.Rows(i)("usr_Node") = 1 Then
                            dRow("Zone") = dtGDDeatils.Rows(0)("Org_Name")
                        End If
                    End If
                End If

                sModuleRole = ""
                If IsDBNull(dtDetails.Rows(i)("usr_LevelGrp")) = False Then
                    If IsDBNull(dtDetails.Rows(i)("Usr_MasterModule")) = False Then
                        If (dtDetails.Rows(i)("Usr_MasterModule") = 1) Then
                            sModuleRole = "Master(" & dtDetails.Rows(i)("MasterRole") & "), "
                        End If
                    End If
                    If IsDBNull(dtDetails.Rows(i)("Usr_PurchaseModule")) = False Then
                        If (dtDetails.Rows(i)("Usr_PurchaseModule") = 1) Then
                            sModuleRole = sModuleRole & "Purchase(" & dtDetails.Rows(i)("PurchaseRole") & "), "
                        End If
                    End If
                    If IsDBNull(dtDetails.Rows(i)("Usr_SalesModule")) = False Then
                        If (dtDetails.Rows(i)("Usr_SalesModule") = 1) Then
                            sModuleRole = sModuleRole & "Sales(" & dtDetails.Rows(i)("SalesRole") & "), "
                        End If
                    End If
                    If IsDBNull(dtDetails.Rows(i)("Usr_AccountsModule")) = False Then
                        If (dtDetails.Rows(i)("Usr_AccountsModule") = 1) Then
                            sModuleRole = sModuleRole & "Accounts(" & dtDetails.Rows(i)("AccountsRole") & "), "
                        End If
                    End If

                    sModuleRole = sModuleRole.Trim
                    If sModuleRole.EndsWith(",") Then
                        sModuleRole = sModuleRole.Remove(Len(sModuleRole) - 1, 1)
                    End If
                End If
                dRow("Module") = sModuleRole
                If IsDBNull(dtDetails.Rows(i)("USR_LastLoginDate")) = False Then
                    dRow("LastLogin") = objclsGRACeGeneral.FormatDtForRDBMS(dtDetails.Rows(i)("USR_LastLoginDate"), "F")
                End If
                If IsDBNull(dtDetails.Rows(i)("Usr_DutyStatus")) = False Then
                    If dtDetails.Rows(i)("Usr_DutyStatus") = "W" Then
                        dRow("Status") = "Waiting for Approval"
                    ElseIf dtDetails.Rows(i)("Usr_DutyStatus") = "D" Then
                        dRow("Status") = "De-Activated"
                    ElseIf (dtDetails.Rows(i)("Usr_DutyStatus") = "A") Then
                        dRow("Status") = "Activated"
                    ElseIf dtDetails.Rows(i)("Usr_DutyStatus") = "L" Then
                        dRow("Status") = "Lock"
                    ElseIf dtDetails.Rows(i)("Usr_DutyStatus") = "B" Then
                        dRow("Status") = "Block"
                    End If
                End If
                dt.Rows.Add(dRow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllUserDetails(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim dt As New DataTable, dtZoneRegionBranchAreaDetails As New DataTable, dtDetails As New DataTable
        Dim dRow As DataRow
        Dim sSql As String, sModuleRole As String = ""
        Try
            dtZoneRegionBranchAreaDetails = GetZoneRegionAreaBranch(sAC, iACID)

            dt.Columns.Add("SrNo")
            dt.Columns.Add("SAPCode")
            dt.Columns.Add("EmpID")
            dt.Columns.Add("EmployeeName")
            dt.Columns.Add("LoginName")
            dt.Columns.Add("Designation")
            dt.Columns.Add("Module")
            dt.Columns.Add("LastLogin")
            dt.Columns.Add("Status")

            sSql = "Select a.usr_id,a.usr_node,(a.Usr_FullName + ' - ' + a.Usr_Code) as FullName,a.Usr_Role,a.usr_FullName,a.Usr_LoginName,a.usr_Code,a.usr_DutyStatus,a.usr_Node,a.Usr_OrgnID,a.usr_LevelGrp,a.usr_GrpOrUserLvlPerm,"
            sSql = sSql & " a.Usr_MasterModule,a.Usr_MasterRole,a.Usr_AuditModule,a.Usr_AuditRole,a.Usr_RiskModule,a.Usr_RiskRole,a.usr_delFlag,a.Usr_ComplianceModule,a.Usr_ComplianceRole,a.Usr_BCMModule,a.Usr_BCMRole,"
            sSql = sSql & " a.usr_DelFlag,a.USR_LastLoginDate,b.mas_Description as Designation,d.mas_Description As MasterRole,e.mas_Description As AuditRole,"
            sSql = sSql & " f.mas_Description As RiskRole,g.mas_Description As ComplianceRole,h.mas_Description As BCMRole, a.USR_LastLoginDate from sad_userdetails a "
            sSql = sSql & " Left Join SAD_GRPDESGN_General_Master b On a.usr_Designation=b.mas_ID "
            sSql = sSql & " left join SAD_GrpOrLvl_General_Master d On a.Usr_MasterRole=d.mas_ID"
            sSql = sSql & " left join SAD_GrpOrLvl_General_Master e On a.Usr_AuditRole=e.mas_ID"
            sSql = sSql & " left join SAD_GrpOrLvl_General_Master f On a.Usr_RiskRole=f.mas_ID"
            sSql = sSql & " left join SAD_GrpOrLvl_General_Master g On a.Usr_ComplianceRole=g.mas_ID"
            sSql = sSql & " left join SAD_GrpOrLvl_General_Master h On a.Usr_BCMRole=h.mas_ID "
            sSql = sSql & " where Usr_CompID=" & iACID & " and Usr_Node=0 and Usr_OrgnID=0 "
            sSql = sSql & " order by FullName"

            dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)

            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                dRow("SrNo") = i + 1
                If IsDBNull(dtDetails.Rows(i)("usr_Code")) = False Then
                    dRow("SAPCode") = dtDetails.Rows(i)("usr_Code")
                End If
                If IsDBNull(dtDetails.Rows(i)("usr_Code")) = False Then
                    dRow("EmpID") = dtDetails.Rows(i)("usr_id")
                End If
                If IsDBNull(dtDetails.Rows(i)("usr_FullName")) = False Then
                    dRow("EmployeeName") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("usr_FullName"))
                End If
                If IsDBNull(dtDetails.Rows(i)("Usr_LoginName")) = False Then
                    dRow("LoginName") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("Usr_LoginName"))
                End If
                If IsDBNull(dtDetails.Rows(i)("Designation")) = False Then
                    dRow("Designation") = dtDetails.Rows(i)("Designation")
                End If

                sModuleRole = ""
                If IsDBNull(dtDetails.Rows(i)("usr_LevelGrp")) = False Then
                    If IsDBNull(dtDetails.Rows(i)("Usr_MasterModule")) = False Then
                        If (dtDetails.Rows(i)("Usr_MasterModule") = 1) Then
                            sModuleRole = "Master(" & dtDetails.Rows(i)("MasterRole") & "), "
                        End If
                    End If
                    If IsDBNull(dtDetails.Rows(i)("Usr_AuditModule")) = False Then
                        If (dtDetails.Rows(i)("Usr_AuditModule") = 1) Then
                            sModuleRole = sModuleRole & "Audit(" & dtDetails.Rows(i)("AuditRole") & "), "
                        End If
                    End If
                    If IsDBNull(dtDetails.Rows(i)("Usr_RiskModule")) = False Then
                        If (dtDetails.Rows(i)("Usr_RiskModule") = 1) Then
                            sModuleRole = sModuleRole & "Risk(" & dtDetails.Rows(i)("RiskRole") & "), "
                        End If
                    End If
                    If IsDBNull(dtDetails.Rows(i)("Usr_ComplianceModule")) = False Then
                        If (dtDetails.Rows(i)("Usr_ComplianceModule") = 1) Then
                            sModuleRole = sModuleRole & "Compliance(" & dtDetails.Rows(i)("ComplianceRole") & "), "
                        End If
                    End If
                    If IsDBNull(dtDetails.Rows(i)("Usr_BCMmodule")) = False Then
                        If (dtDetails.Rows(i)("Usr_BCMmodule") = 1) Then
                            sModuleRole = sModuleRole & "BCM(" & dtDetails.Rows(i)("BCMRole") & ")"
                        End If
                    End If
                    sModuleRole = sModuleRole.Trim
                    If sModuleRole.EndsWith(",") Then
                        sModuleRole = sModuleRole.Remove(Len(sModuleRole) - 1, 1)
                    End If
                End If
                dRow("Module") = sModuleRole
                If IsDBNull(dtDetails.Rows(i)("USR_LastLoginDate")) = False Then
                    dRow("LastLogin") = objclsGRACeGeneral.FormatDtForRDBMS(dtDetails.Rows(i)("USR_LastLoginDate"), "F")
                End If
                If IsDBNull(dtDetails.Rows(i)("Usr_DutyStatus")) = False Then
                    If dtDetails.Rows(i)("Usr_DutyStatus") = "W" Then
                        dRow("Status") = "Waiting for Approval"
                    ElseIf dtDetails.Rows(i)("Usr_DutyStatus") = "D" Then
                        dRow("Status") = "De-Activated"
                    ElseIf (dtDetails.Rows(i)("Usr_DutyStatus") = "A") Then
                        dRow("Status") = "Activated"
                    ElseIf dtDetails.Rows(i)("Usr_DutyStatus") = "L" Then
                        dRow("Status") = "Lock"
                    ElseIf dtDetails.Rows(i)("Usr_DutyStatus") = "B" Then
                        dRow("Status") = "Block"
                    End If
                End If
                dt.Rows.Add(dRow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetZoneRegionAreaBranch(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select Org_Node,Org_Name,Org_Parent,Org_levelCode from sad_org_Structure where Org_CompID=" & iACID & " and Org_levelCode <> ''"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetZoneRegionAreaBranchNameFromID(ByVal sAC As String, ByVal iACID As Integer, ByVal iLevelCode As Integer, ByVal iOrgID As String) As String
        Dim sSql As String
        Try
            sSql = "Select Org_Name from sad_org_Structure where Org_LevelCode= " & iLevelCode & " And Org_DelFlag='A' and Org_CompId=" & iACID & " and org_node=" & iOrgID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetZoneRegionAreaBranchID(ByVal sAC As String, ByVal iACID As Integer, ByVal iLevelCode As Integer, ByVal sOrgName As String) As Integer
        Dim sSql As String
        Try
            sSql = "Select org_node from sad_org_Structure where Org_LevelCode= " & iLevelCode & " and Org_DelFlag='A' and Org_CompId=" & iACID & " and Org_Name Like '" & sOrgName & "'"
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadZoneMaster(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select org_node,Org_Name from sad_org_Structure where Org_LevelCode=1 and Org_DelFlag='A' and Org_CompId=" & iACID & " order by Org_Name"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadRegioAreaBranchMaster(ByVal sAC As String, ByVal iACID As Integer, ByVal iParent As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select org_node,Org_Name from sad_org_Structure where Org_Parent=" & iParent & " and Org_DelFlag='A' and Org_CompId=" & iACID & " order by Org_Name"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadExistingEmployee(ByVal sAC As String, ByVal iACID As Integer, ByVal iZoneID As Integer, ByVal iRegionID As Integer,
                                          ByVal iAreaID As Integer, ByVal iBranchID As Integer, ByVal sSearch As String) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Usr_ID,(Usr_FullName + ' - ' + Usr_Code) as FullName from Sad_UserDetails Where Usr_CompID=" & iACID & " "
            If iZoneID > 0 Then
                sSql = sSql & " And usr_node=1 And usr_OrgnID=" & iZoneID & ""
            End If
            If iRegionID > 0 Then
                sSql = sSql & " And usr_node=2 And usr_OrgnID=" & iRegionID & ""
            End If
            If iAreaID > 0 Then
                sSql = sSql & " and  usr_node=3 And usr_OrgnID=" & iAreaID & ""
            End If
            If iBranchID > 0 Then
                sSql = sSql & " And usr_node=4 And usr_OrgnID=" & iBranchID & ""
            End If
            If sSearch <> "" Then
                sSql = sSql & " And (Usr_FullName like '" & sSearch & "%' Or Usr_Code like '" & sSearch & "%')"
            End If
            sSql = sSql & " and Usr_Node>0 and Usr_OrgnID>0 order by Usr_FullName "
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadExistingUsers(ByVal sAC As String, ByVal iACID As Integer, ByVal iZoneID As Integer, ByVal iRegionID As Integer,
                                      ByVal iAreaID As Integer, ByVal iBranchID As Integer, ByVal sSearch As String) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Usr_ID,(Usr_FullName + ' - ' + Usr_Code) as FullName from Sad_UserDetails Where Usr_CompID=" & iACID & " and Usr_Node=0 and Usr_OrgnID=0 "
            If sSearch <> "" Then
                sSql = sSql & " And (Usr_FullName Like '" & sSearch & "%' Or Usr_Code like '" & sSearch & "%')"
            End If
            sSql = sSql & "  order by Usr_FullName "
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveEmployeeDetails(ByVal sAC As String, ByVal objEmp As clsEmployeeMaster)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(32) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objEmp.iUserID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_Node", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objEmp.iUsrNode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_Code", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objEmp.sUsrCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_FullName", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objEmp.sUsrFullName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_LoginName", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objEmp.sUsrLoginName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_Password", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objEmp.sUsrPassword
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_Email", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objEmp.sUsrEmail
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_LevelGrp", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objEmp.iUsrLevelGrp
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_DutyStatus", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objEmp.sUsrDutyStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_PhoneNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objEmp.sUsrPhoneNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_MobileNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objEmp.sUsrMobileNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_OfficePhone", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objEmp.sUsrOfficePhone
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_OffPhExtn", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objEmp.sUsrOffPhExtn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_Designation", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objEmp.iUsrDesignation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_OrgnID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objEmp.iUsrOrgID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_GrpOrUserLvlPerm", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objEmp.iUsrGrpOrUserLvlPerm
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_Role", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objEmp.iUsrRole
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_MasterModule", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objEmp.iUsrMasterModule
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_PurchaseModule", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objEmp.iUsrPurchaseModule
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_SalesModule", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objEmp.iUsrSalesModule
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_AccountsModule", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objEmp.iUsrAccountsModule
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_MasterRole", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objEmp.iUsrMasterRole
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_PurchaseRole", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objEmp.iUsrPurchaseRole
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_SalesRole", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objEmp.iUsrSalesRole
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_AccountsRole", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objEmp.iUsrAccountsRole
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objEmp.iUsrCreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objEmp.iUsrCreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_DelFlag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objEmp.sUsrFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_Status", OleDb.OleDbType.VarChar, 3)
            ObjParam(iParamCount).Value = objEmp.sUsrStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_IPAddress", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objEmp.Usr_IPAdress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Usr_CompId", OleDb.OleDbType.Integer, 50)
            ObjParam(iParamCount).Value = objEmp.iUsrCompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spEmployeeMaster", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadExistingEmployeeDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select * from Sad_UserDetails where  Usr_ID=" & iUserID & " and Usr_CompId=" & iACID & ""
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetParentID(ByVal sAC As String, ByVal iACID As Integer, ByVal iNode As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select Org_Parent from sad_org_Structure where org_node=" & iNode & " and Org_CompId=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckSAPCode(ByVal sAC As String, ByVal iACID As Integer, ByVal sSAPCode As String) As Boolean
        Dim sSql As String
        Try
            sSql = "Select Usr_ID from Sad_UserDetails where Upper(Usr_Code)='" & sSAPCode & "' and Usr_CompId=" & iACID & ""
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetUesrPassword(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As String) As String
        Dim sSql As String
        Try
            sSql = "Select usr_password from Sad_Userdetails where Usr_ID=" & iUserID & " and Usr_CompId=" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdatePasswordReset(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iPasswordReset As Integer)
        Dim sSql As String
        Try
            sSql = "Update Sad_UserDetails set Usr_Status='N',Usr_IsPasswordReset=" & iPasswordReset & " where Usr_ID=" & iUserID & " and Usr_CompId=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function CheckForLoginName(ByVal sAC As String, ByVal iACID As Integer, ByVal sLoginName As String) As Boolean
        Dim sSql As String = ""
        Try
            sSql = "Select Usr_ID from Sad_UserDetails where Upper(usr_LoginName)='" & sLoginName & "' and Usr_CompId=" & iACID & ""
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub EmployeeApproveStatus(ByVal sAC As String, ByVal iACID As Integer, ByVal iSessionUsrID As Integer, ByVal iEmployeeID As Integer, ByVal sIPAddress As String, ByVal sType As String)
        Dim sSql As String
        Try
            sSql = "Update Sad_Userdetails set"
            If sType = "Created" Then
                sSql = sSql & " Usr_DelFlag='A',Usr_Status='A',Usr_DutyStatus='A',usr_AppBy=" & iSessionUsrID & ", usr_AppOn=Getdate(),"
            ElseIf sType = "DeActivated" Then
                sSql = sSql & " Usr_DelFlag='D',Usr_Status='AD',Usr_DutyStatus='D',Usr_DisableBy=" & iSessionUsrID & ", Usr_DisableDate=Getdate(),"
            ElseIf sType = "Activated" Then
                sSql = sSql & " Usr_DelFlag='A',Usr_Status='AR',Usr_DutyStatus='A',usr_AppBy=" & iSessionUsrID & ", usr_AppOn=Getdate(),"
            ElseIf sType = "UnBlock" Then
                sSql = sSql & " Usr_DutyStatus='A',Usr_Status='UB',usr_NoOfUnSucsfAtteptts=0,USR_LastLoginDate=GetDate(),usr_UnBlockLockBy=" & iSessionUsrID & ",usr_UnBlockLockOn=GetDate(),"
            ElseIf sType = "UnLock" Then
                sSql = sSql & " Usr_DutyStatus='A',Usr_Status='UL',usr_NoOfUnSucsfAtteptts=0,USR_LastLoginDate=GetDate(),usr_UnBlockLockBy=" & iSessionUsrID & ",usr_UnBlockLockOn=GetDate(),"
            End If
            sSql = sSql & "Usr_IPAddress='" & sIPAddress & "' Where Usr_CompId=" & iACID & " And Usr_ID=" & iEmployeeID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class
