Imports System
Imports DatabaseLayer
Imports System.Data
Public Class clsModulePermission
    Private objDBL As New DatabaseLayer.DBHelper
    Private objclsGeneralFunctions As New clsGeneralFunctions

    Dim SGP_ID As Integer
    Dim SGP_ModID As Integer
    Dim SGP_LevelGroup As String
    Dim SGP_LevelGroupID As Integer
    Dim SGP_View As Integer
    Dim SGP_New As Integer
    Dim SGP_SaveOrUpdate As Integer
    Dim SGP_Approve As Integer
    Dim SGP_ActivateOrDeactivate As Integer
    Dim SGP_Report As Integer
    Dim SGP_Download As Integer
    Dim SGP_Annotation As Integer
    Dim SGP_Exception As Integer
    Dim SGP_CreatedBy As Integer
    Dim SGP_ApprovedBy As Integer
    Dim SGP_UpdatedBy As Integer
    Dim SGP_Status As String
    Dim SGP_DelFlag As String
    Dim SGP_CompID As Integer

    Public Property iSGP_CompID() As Integer
        Get
            Return (SGP_CompID)
        End Get
        Set(ByVal Value As Integer)
            SGP_CompID = Value
        End Set
    End Property
    Public Property sSGP_DelFlag() As String
        Get
            Return (SGP_DelFlag)
        End Get
        Set(ByVal Value As String)
            SGP_DelFlag = Value
        End Set
    End Property
    Public Property sSGP_Status() As String
        Get
            Return (SGP_Status)
        End Get
        Set(ByVal Value As String)
            SGP_Status = Value
        End Set
    End Property
    Public Property iSGP_UpdatedBy() As Integer
        Get
            Return (SGP_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            SGP_UpdatedBy = Value
        End Set
    End Property
    Public Property iSGP_ApprovedBy() As Integer
        Get
            Return (SGP_ApprovedBy)
        End Get
        Set(ByVal Value As Integer)
            SGP_ApprovedBy = Value
        End Set
    End Property
    Public Property iSGP_CreatedBy() As Integer
        Get
            Return (SGP_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            SGP_CreatedBy = Value
        End Set
    End Property
    Public Property iSGP_Report() As Integer
        Get
            Return (SGP_Report)
        End Get
        Set(ByVal Value As Integer)
            SGP_Report = Value
        End Set
    End Property
    Public Property iSGP_Download() As Integer
        Get
            Return (SGP_Download)
        End Get
        Set(ByVal Value As Integer)
            SGP_Download = Value
        End Set
    End Property
    Public Property iSGP_Annotation() As Integer
        Get
            Return (SGP_Annotation)
        End Get
        Set(ByVal Value As Integer)
            SGP_Annotation = Value
        End Set
    End Property

    Public Property iSGP_Exception() As Integer
        Get
            Return (SGP_Exception)
        End Get
        Set(ByVal Value As Integer)
            SGP_Exception = Value
        End Set
    End Property
    Public Property iSGP_Approve() As Integer
        Get
            Return (SGP_Approve)
        End Get
        Set(ByVal Value As Integer)
            SGP_Approve = Value
        End Set
    End Property

    Public Property iSGP_ActivateOrDeactivate() As Integer
        Get
            Return (SGP_ActivateOrDeactivate)
        End Get
        Set(ByVal Value As Integer)
            SGP_ActivateOrDeactivate = Value
        End Set
    End Property
    Public Property iSGP_SaveOrUpdate() As Integer
        Get
            Return (SGP_SaveOrUpdate)
        End Get
        Set(ByVal Value As Integer)
            SGP_SaveOrUpdate = Value
        End Set
    End Property
    Public Property iSGP_View() As Integer
        Get
            Return (SGP_View)
        End Get
        Set(ByVal Value As Integer)
            SGP_View = Value
        End Set
    End Property
    Public Property iSGP_New() As Integer
        Get
            Return (SGP_New)
        End Get
        Set(ByVal Value As Integer)
            SGP_New = Value
        End Set
    End Property
    Public Property iSGP_LevelGroupID() As Integer
        Get
            Return (SGP_LevelGroupID)
        End Get
        Set(ByVal Value As Integer)
            SGP_LevelGroupID = Value
        End Set
    End Property
    Public Property sSGP_LevelGroup() As String
        Get
            Return (SGP_LevelGroup)
        End Get
        Set(ByVal Value As String)
            SGP_LevelGroup = Value
        End Set
    End Property
    Public Property iSGP_ModID() As Integer
        Get
            Return (SGP_ModID)
        End Get
        Set(ByVal Value As Integer)
            SGP_ModID = Value
        End Set
    End Property
    Public Property iSGP_ID() As Integer
        Get
            Return (SGP_ID)
        End Get
        Set(ByVal Value As Integer)
            SGP_ID = Value
        End Set
    End Property
    Public Function LoadActiveRole(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "select mas_id,mas_desc from acc_general_master where Mas_master=6 and  Mas_Delflag='A' and Mas_CompID=" & iACID & ""
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadModules(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select Mod_ID,Mod_Parent,Mod_Description,Mod_Code,Mod_Notes From Sad_Module Where Mod_Parent=0 And Mod_DelFlag='X' and "
            sSql = sSql & "Mod_CompID =" & iACID & " order by Mod_ID"
            Return (objDBL.SQLExecuteDataTable(sAC, sSql))
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Sub GetAllModule(ByVal sAC As String, ByVal iACID As Integer, ByVal iParentID As Integer, ByRef dtFinalTab As DataTable)
        Dim ds As New DataSet
        Dim dr As DataRow
        Dim sSql As String
        Try
            sSql = "Select * From Sad_Module Where Mod_CompID=" & iACID & " And  Mod_Parent = " & iParentID & " And Mod_DelFlag='X'"
            ds = objDBL.SQLExecuteDataSet(sAC, sSql)

            Dim dv As New DataView(ds.Tables(0))
            Dim drv As DataRowView

            For Each drv In dv
                If dtFinalTab Is Nothing = False Then
                    dr = dtFinalTab.NewRow
                    dr("Mod_Id") = drv("Mod_ID")
                    dr("Mod_Description") = drv("Mod_Description")
                    dr("mod_Function") = drv("Mod_NavFunc")
                    dr("Mod_Buttons") = drv("Mod_Buttons")
                    dtFinalTab.Rows.Add(dr)
                    PopulateChildModules(sAC, iACID, drv("Mod_ID"), dtFinalTab)
                End If
            Next
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub PopulateChildModules(ByVal sAC As String, ByVal iACID As Integer, ByVal iParentID As Integer, ByRef dtFinalTab As DataTable)
        Dim ds As New DataSet
        Dim dr As DataRow
        Dim sSql As String
        Try
            sSql = "Select * From Sad_Module Where Mod_CompID=" & iACID & " And  Mod_Parent = " & iParentID & " And Mod_DelFlag='X'"
            ds = objDBL.SQLExecuteDataSet(sAC, sSql)

            Dim dv As New DataView(ds.Tables(0))
            Dim drv As DataRowView

            For Each drv In dv
                If dtFinalTab Is Nothing = False Then
                    dr = dtFinalTab.NewRow
                    dr("Mod_Id") = drv("Mod_ID")
                    dr("Mod_Description") = drv("Mod_Description")
                    dr("mod_Function") = drv("Mod_NavFunc")
                    dr("Mod_Buttons") = drv("Mod_Buttons")
                    dtFinalTab.Rows.Add(dr)
                    PopulateChildModules(sAC, iACID, drv("Mod_ID"), dtFinalTab)
                End If
            Next
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadUserDetails(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select Usr_ID,(Usr_FullName + ' - ' + Usr_Code) as FullName from Sad_UserDetails Where Usr_DutyStatus = 'A' and "
            sSql = sSql & " Usr_GrpOrUserLvlPerm='1' and Usr_CompId=" & iACID & " order by Usr_FullName"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function GetCheckPermission(ByVal sAC As String, ByVal iACID As Integer, ByVal iModId As Integer, ByVal iUsrorGrpID As Integer, ByVal sPType As String)
    '    Dim sSql As String
    '    Dim ds As New DataSet
    '    Try
    '        sSql = "Select * From SAD_UsrOrGrp_Permission Where Perm_CompID=" & iACID & " And  Perm_PType = '" & sPType & "' and "
    '        sSql = sSql & "Perm_UsrORGrpID = " & iUsrorGrpID & " and Perm_ModuleID = " & iModId & ""
    '        ds = objDBL.SQLExecuteDataSet(sAC, sSql)
    '        Return ds
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function GetOperation(ByVal sAC As String, ByVal iACID As Integer, ByVal iModID As Integer) As DataSet
        Dim sSql As String
        Dim ds As New DataSet
        Try
            sSql = "Select op_PkID,Op_OperationName From SAD_Mod_Operations Where OP_CompID=" & iACID & " And OP_Status='A' And  Op_ModuleID = " & iModID & ""
            ds = objDBL.SQLExecuteDataSet(sAC, sSql)
            Return ds
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Sub DeletePermission(ByVal sAC As String, ByVal iACID As Integer, ByVal sPermissionType As String, ByVal iUsrOrGrpID As Integer, ByVal iModuleID As Integer)
    '    Dim blnRetValue As Boolean = False
    '    Dim sStr As String
    '    Try
    '        sStr = "DELETE From SAD_UsrOrGrp_Permission Where Perm_CompID=" & iACID & " And  Perm_PType='" & sPermissionType & "' AND "
    '        sStr = sStr & "Perm_UsrOrGrpID =" & iUsrOrGrpID & " And Perm_ModuleID=" & iModuleID & ""
    '        objDBL.SQLExecuteNonQuery(sAC, sStr)
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    'Public Function SaveOrUpdatePermission(ByVal sAC As String, ByVal iACID As Integer, ByVal sPermType As String, ByVal UsrORGrpID As Integer,
    '                                              ByVal ModuleID As Integer, ByVal iOperationIDs As String, ByVal iCrBy As Integer, ByVal sIPAddress As String) As String
    '    Dim blnRetValue As Boolean = False
    '    Dim sStr As String = ""
    '    Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(9) {}
    '    Dim iParamCount As Integer
    '    Dim iRet As Integer
    '    Try
    '        Dim sModPkIDs() As String
    '        If iOperationIDs.StartsWith(";") = False Then
    '            iOperationIDs = ";" & iOperationIDs
    '        End If
    '        If iOperationIDs.EndsWith(";") = False Then
    '            iOperationIDs = iOperationIDs & ";"
    '        End If
    '        sModPkIDs = Split(iOperationIDs, ";")
    '        For i As Integer = 1 To UBound(sModPkIDs) - 1

    '            iParamCount = 0
    '            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Perm_PType", OleDb.OleDbType.VarChar, 1)
    '            ObjParam(iParamCount).Value = sPermType
    '            ObjParam(iParamCount).Direction = ParameterDirection.Input
    '            iParamCount += 1

    '            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Perm_UsrORGrpID", OleDb.OleDbType.Integer, 4)
    '            ObjParam(iParamCount).Value = UsrORGrpID
    '            ObjParam(iParamCount).Direction = ParameterDirection.Input
    '            iParamCount += 1

    '            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Perm_ModuleID", OleDb.OleDbType.Integer, 4)
    '            ObjParam(iParamCount).Value = ModuleID
    '            ObjParam(iParamCount).Direction = ParameterDirection.Input
    '            iParamCount += 1

    '            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Perm_OpPKID", OleDb.OleDbType.Integer, 4)
    '            ObjParam(iParamCount).Value = sModPkIDs(i)
    '            ObjParam(iParamCount).Direction = ParameterDirection.Input
    '            iParamCount += 1

    '            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Perm_Status", OleDb.OleDbType.VarChar, 1)
    '            ObjParam(iParamCount).Value = "A"
    '            ObjParam(iParamCount).Direction = ParameterDirection.Input
    '            iParamCount += 1


    '            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Perm_Crby", OleDb.OleDbType.Integer, 1)
    '            ObjParam(iParamCount).Value = iCrBy
    '            ObjParam(iParamCount).Direction = ParameterDirection.Input
    '            iParamCount += 1

    '            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Perm_CrOn", OleDb.OleDbType.Date, 8)
    '            ObjParam(iParamCount).Value = Date.Today
    '            ObjParam(iParamCount).Direction = ParameterDirection.Input
    '            iParamCount += 1

    '            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Perm_CompID", OleDb.OleDbType.Integer, 1)
    '            ObjParam(iParamCount).Value = iACID
    '            ObjParam(iParamCount).Direction = ParameterDirection.Input
    '            iParamCount += 1

    '            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Perm_IPAddress", OleDb.OleDbType.VarChar, 25)
    '            ObjParam(iParamCount).Value = sIPAddress
    '            ObjParam(iParamCount).Direction = ParameterDirection.Input
    '            iParamCount += 1

    '            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
    '            ObjParam(iParamCount).Direction = ParameterDirection.Output

    '            iRet = objDBL.ExecuteSPForInsert(sAC, "spSAD_UsrOrGrp_Permission", "@iOper", ObjParam)
    '        Next
    '        Return iRet
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function LoadAllModuleUsers(ByVal sAC As String, ByVal iACID As Integer, ByVal iModuleID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select  usr_Id,(Usr_FullName + ' - ' + Usr_Code) As usr_FullName,Usr_MasterModule,Usr_AuditModule,Usr_RiskModule,Usr_ComplianceModule,Usr_BCMModule from Sad_UserDetails where"
            If iModuleID = 1 Then
                sSql = sSql & " (Usr_MasterModule!='1' or Usr_MasterModule IS NULL) And "
            ElseIf iModuleID = 2 Then
                sSql = sSql & " (Usr_RiskModule!='1' or Usr_RiskModule IS NULL) And "
            ElseIf iModuleID = 3 Then
                sSql = sSql & " (Usr_AuditModule!='1' or Usr_AuditModule IS NULL) And "
            ElseIf iModuleID = 4 Then
                sSql = sSql & " (Usr_ComplianceModule!='1' or Usr_ComplianceModule IS NULL) And "
            ElseIf iModuleID = 5 Then
                sSql = sSql & " (Usr_BCMModule!='1' or Usr_BCMModule IS NULL) And "
            End If
            sSql = sSql & " (Usr_DutyStatus='A' or Usr_DutyStatus='B' or Usr_DutyStatus='L') order by usr_FullName"
            Return (objDBL.SQLExecuteDataTable(sAC, sSql))

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadModuleUsers(ByVal sAC As String, ByVal iACID As Integer, ByVal iModule As Integer, ByVal sSearch As String) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select usr_Id,(Usr_FullName + ' - ' + Usr_Code) As usr_FullName,Usr_MasterModule,Usr_AuditModule,Usr_RiskModule,Usr_ComplianceModule,Usr_BCMModule from Sad_UserDetails where"
            If iModule = 1 Then
                sSql = sSql & " (Usr_MasterModule!='1' or Usr_MasterModule IS NULL) and usr_FullName  like '" & sSearch & "%' and usr_CompID=" & iACID & " And "
            ElseIf iModule = 2 Then
                sSql = sSql & " (Usr_RiskModule!='1' or Usr_RiskModule IS NULL) and usr_FullName  like '" & sSearch & "%' and usr_CompID=" & iACID & " And "
            ElseIf iModule = 3 Then
                sSql = sSql & " (Usr_AuditModule!='1' or Usr_AuditModule IS NULL) and usr_FullName  like '" & sSearch & "%' and usr_CompID=" & iACID & " And "
            ElseIf iModule = 4 Then
                sSql = sSql & " (Usr_ComplianceModule!='1' or Usr_ComplianceModule IS NULL) and usr_FullName  like '" & sSearch & "%' and usr_CompID=" & iACID & " And "
            ElseIf iModule = 5 Then
                sSql = sSql & " (Usr_BCMModule!='1' or Usr_BCMModule IS NULL) and usr_FullName  like '" & sSearch & "%' and usr_CompID=" & iACID & " And "
            Else
                sSql = sSql & " Usr_FullName  like '" & sSearch & "%' and usr_CompID=" & iACID & " And "
            End If
            sSql = sSql & " (Usr_DutyStatus ='A' or Usr_DutyStatus='B' or Usr_DutyStatus='L') order by usr_FullName"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadUserPermissionDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iModuleID As Integer) As DataTable
        Dim sSql As String
        Dim dtDetails As New DataTable
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim i As Integer
        Try
            dt.Columns.Add("Sr.No")
            dt.Columns.Add("usr_Id")
            dt.Columns.Add("SAP Code")
            dt.Columns.Add("Login Name")
            dt.Columns.Add("User full name")
            dt.Columns.Add("Designation")
            dt.Columns.Add("Module Role")

            sSql = "Select usr_Id,usr_Code,usr_LoginName,usr_FullName,usr_Designation,Usr_Role,Usr_MasterModule,Usr_MasterRole,Usr_AuditRole,Usr_RiskRole,Usr_ComplianceRole,Usr_BCMRole,"
            sSql = sSql & " Usr_AuditModule,Usr_RiskModule,Usr_ComplianceModule,Usr_BCMModule,b.Mas_Description As Designation,c.Mas_Description As Role from Sad_UserDetails a"
            sSql = sSql & " Left Join SAD_GRPDESGN_General_Master b on b.Mas_ID=a.usr_Designation"
            sSql = sSql & " Left Join SAD_GrpOrLvl_General_Master c on c.Mas_ID=a.Usr_Role"

            If iModuleID = 1 Then
                sSql = sSql & " where Usr_MasterModule='1' and usr_CompID=" & iACID & ""
            ElseIf iModuleID = 2 Then
                sSql = sSql & " where Usr_RiskModule='1' and usr_CompID=" & iACID & ""
            ElseIf iModuleID = 3 Then
                sSql = sSql & " where Usr_AuditModule='1' and usr_CompID=" & iACID & ""
            ElseIf iModuleID = 4 Then
                sSql = sSql & " where Usr_ComplianceModule='1' and usr_CompID=" & iACID & ""
            ElseIf iModuleID = 5 Then
                sSql = sSql & " where Usr_BCMModule='1' and usr_CompID=" & iACID & ""
            End If
            sSql = sSql & " order by usr_FullName"

            dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtDetails.Rows.Count > 0 Then
                For i = 0 To dtDetails.Rows.Count - 1
                    dRow = dt.NewRow()
                    dRow("Sr.No") = i + 1
                    If IsDBNull(dtDetails.Rows(i)("usr_Id")) = False Then
                        dRow("usr_Id") = dtDetails.Rows(i)("usr_Id")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("usr_Code")) = False Then
                        dRow("SAP Code") = dtDetails.Rows(i)("usr_Code")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("usr_LoginName")) = False Then
                        dRow("Login Name") = dtDetails.Rows(i)("usr_LoginName")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("usr_FullName")) = False Then
                        dRow("User full name") = dtDetails.Rows(i)("usr_FullName")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("Designation")) = False Then
                        dRow("Designation") = dtDetails.Rows(i)("Designation")
                    End If
                    If iModuleID = 1 Then
                        If IsDBNull(dtDetails.Rows(i)("Usr_MasterRole")) = False Then
                            dRow("Module Role") = objDBL.GetColumnDescription(sAC, "Mas_Description", "Mas_ID", dtDetails.Rows(i)("Usr_MasterRole"), "SAD_GrpOrLvl_General_Master")
                        End If
                    ElseIf iModuleID = 2 Then
                        If IsDBNull(dtDetails.Rows(i)("Usr_RiskRole")) = False Then
                            dRow("Module Role") = objDBL.GetColumnDescription(sAC, "Mas_Description", "Mas_ID", dtDetails.Rows(i)("Usr_RiskRole"), "SAD_GrpOrLvl_General_Master")
                        End If
                    ElseIf iModuleID = 3 Then
                        If IsDBNull(dtDetails.Rows(i)("Usr_AuditRole")) = False Then
                            dRow("Module Role") = objDBL.GetColumnDescription(sAC, "Mas_Description", "Mas_ID", dtDetails.Rows(i)("Usr_AuditRole"), "SAD_GrpOrLvl_General_Master")
                        End If
                    ElseIf iModuleID = 4 Then
                        If IsDBNull(dtDetails.Rows(i)("Usr_ComplianceRole")) = False Then
                            dRow("Module Role") = objDBL.GetColumnDescription(sAC, "Mas_Description", "Mas_ID", dtDetails.Rows(i)("Usr_ComplianceRole"), "SAD_GrpOrLvl_General_Master")
                        End If
                    ElseIf iModuleID = 5 Then
                        If IsDBNull(dtDetails.Rows(i)("Usr_BCMRole")) = False Then
                            dRow("Module Role") = objDBL.GetColumnDescription(sAC, "Mas_Description", "Mas_ID", dtDetails.Rows(i)("Usr_BCMRole"), "SAD_GrpOrLvl_General_Master")
                        End If
                    End If
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub RemoveUserFromModule(ByVal sAC As String, ByVal iACID As Integer, ByVal iModuleID As Integer, ByVal iUserId As Integer)
        Dim sSql As String = ""
        Try
            If iModuleID = 1 Then
                sSql = "Update Sad_UserDetails set Usr_MasterModule=0,Usr_MasterRole=0 where usr_Id=" & iUserId & " and Usr_CompId=" & iACID & ""
            ElseIf iModuleID = 2 Then
                sSql = "Update Sad_UserDetails set Usr_RiskModule=0,Usr_RiskRole=0 where usr_Id=" & iUserId & " and Usr_CompId=" & iACID & ""
            ElseIf iModuleID = 3 Then
                sSql = "Update Sad_UserDetails set Usr_AuditModule=0,Usr_AuditRole=0 where usr_Id=" & iUserId & " and Usr_CompId=" & iACID & ""
            ElseIf iModuleID = 4 Then
                sSql = "Update Sad_UserDetails set Usr_ComplianceModule=0,Usr_ComplianceRole=0 where usr_Id=" & iUserId & " and Usr_CompId=" & iACID & ""
            ElseIf iModuleID = 5 Then
                sSql = "Update Sad_UserDetails set Usr_BCMModule=0,Usr_BCMRole=0 where usr_Id=" & iUserId & " and Usr_CompId=" & iACID & ""
            End If
            objDBL.SQLExecuteNonQuery(sAC, sSql)
            If objDBL.SQLExecuteScalarInt(sAC, "Select Usr_LevelGrp From Sad_UserDetails Where usr_Id=" & iUserId & " and Usr_CompId=" & iACID & "") = iModuleID Then
                sSql = "" : sSql = "Update Sad_UserDetails Set Usr_LevelGrp=0,Usr_Role=0 where usr_Id=" & iUserId & " And Usr_CompId=" & iACID & ""
                objDBL.SQLExecuteNonQuery(sAC, sSql)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub UpdateModuleToUser(ByVal sAC As String, ByVal iACID As Integer, ByVal iModule As Integer, ByVal iUserID As Integer, ByVal iRoleID As Integer)
        Dim sSql As String = ""
        Dim iCheckModule As Integer
        Try
            iCheckModule = objDBL.SQLExecuteScalarInt(sAC, "Select usr_Id From Sad_UserDetails Where usr_Id=" & iUserID & " And Usr_LevelGrp=0 And Usr_MasterModule=0 And Usr_AuditModule=0 And Usr_RiskModule=0 And Usr_ComplianceModule=0 And Usr_BCMModule=0 And Usr_CompId=" & iACID & "")
            If iModule = 1 Then
                sSql = "Update Sad_UserDetails set Usr_MasterModule=1,Usr_MasterRole=" & iRoleID & " where usr_Id=" & iUserID & " and Usr_CompId=" & iACID & ""
            ElseIf iModule = 2 Then
                sSql = "Update Sad_UserDetails set Usr_RiskModule=1,Usr_RiskRole=" & iRoleID & " where usr_Id=" & iUserID & "  and Usr_CompId=" & iACID & ""
            ElseIf iModule = 3 Then
                sSql = "Update Sad_UserDetails set Usr_AuditModule=1,Usr_AuditRole=" & iRoleID & " where usr_Id=" & iUserID & "  and Usr_CompId=" & iACID & ""
            ElseIf iModule = 4 Then
                sSql = "Update Sad_UserDetails set Usr_ComplianceModule=1,Usr_ComplianceRole=" & iRoleID & " where usr_Id=" & iUserID & "  and Usr_CompId=" & iACID & ""
            ElseIf iModule = 5 Then
                sSql = "Update Sad_UserDetails set Usr_BCMModule=1,Usr_BCMRole=" & iRoleID & " where usr_Id=" & iUserID & "  and Usr_CompId=" & iACID & ""
            End If
            objDBL.SQLExecuteNonQuery(sAC, sSql)
            If iCheckModule > 0 Then
                sSql = "Update Sad_UserDetails set Usr_LevelGrp=" & iModule & ",Usr_Role=" & iRoleID & " where usr_Id=" & iUserID & " and Usr_CompId=" & iACID & ""
                objDBL.SQLExecuteNonQuery(sAC, sSql)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub


    'changed here


    Public Function CheckAvailability(ByVal sNameSpace As String, ByVal sGroup As String, ByVal iUserOrGrpId As Integer) As Integer
        Dim StrSql As String = ""
        Dim dr As OleDb.OleDbDataReader
        Dim iRet As Integer
        Try
            StrSql = "" : StrSql = "Select Sgp_Id,Sgp_ModId,SGP_LevelGroup, SGP_LevelGroupID from Sad_UsrOrGrp_permission where "
            StrSql = StrSql & "SGP_LevelGroup = '" & sGroup & "' and SGP_LevelGroupID =" & iUserOrGrpId & ""
            dr = objDBL.SQLDataReader(sNameSpace, StrSql)
            If dr.HasRows = True Then
                iRet = 1
            Else
                iRet = 0
            End If
            dr.Close()
            Return iRet
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function IsPermissionSet(ByVal sNameSpace As String, ByVal iACID As Integer, ByVal sLvlGrp As String, ByVal iLvlGrpID As Integer, ByVal iModID As Integer) As Boolean
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "select * from Sad_UsrOrGrp_permission where SGP_modID=" & iModID & " and SGP_levelGroup='" & sLvlGrp & "' and SGP_levelGroupID=" & iLvlGrpID & " and SGP_CompID=" & iACID & ""
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub DeletePermission(ByVal sAC As String, ByVal iACID As Integer, ByVal sPermissionType As String, ByVal iUsrOrGrpID As Integer, ByVal iModuleID As Integer)
        Dim blnRetValue As Boolean = False
        Dim sStr As String
        Try
            sStr = "DELETE From SAD_UsrOrGrp_Permission Where SGP_CompID=" & iACID & " And  SGP_levelGroup='" & sPermissionType & "' AND "
            sStr = sStr & "SGP_levelGroupID =" & iUsrOrGrpID & " And SGP_modID=" & iModuleID & ""
            objDBL.SQLExecuteNonQuery(sAC, sStr)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function SaveOrUpdatePermission(ByVal sNameSpace As String, ByVal objPerm As clsModulePermission) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(23) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SGP_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPerm.iSGP_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SGP_ModID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPerm.iSGP_ModID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SGP_LevelGroup", OleDb.OleDbType.VarChar, 4)
            ObjParam(iParamCount).Value = objPerm.sSGP_LevelGroup
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SGP_LevelGroupID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPerm.iSGP_LevelGroupID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SGP_View", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPerm.iSGP_View
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SGP_New", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPerm.iSGP_New
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SGP_SaveOrUpdate", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPerm.iSGP_SaveOrUpdate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SGP_Approve", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPerm.iSGP_Approve
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SGP_ActivateOrDeactivate", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPerm.iSGP_ActivateOrDeactivate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SGP_Report ", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPerm.iSGP_Report
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SGP_Download ", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPerm.iSGP_Download
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SGP_Annotaion ", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPerm.iSGP_Annotation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SGP_Exception ", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPerm.iSGP_Exception
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SGP_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPerm.iSGP_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SGP_CreatedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = Date.Today
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SGP_ApprovedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPerm.iSGP_ApprovedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SGP_ApprovedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = Date.Today
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SGP_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPerm.iSGP_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SGP_UpdatedOn", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = Date.Today
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SGP_Status", OleDb.OleDbType.VarChar, 4)
            ObjParam(iParamCount).Value = objPerm.sSGP_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SGP_DelFlag", OleDb.OleDbType.VarChar, 4)
            ObjParam(iParamCount).Value = objPerm.sSGP_DelFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@SGP_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objPerm.iSGP_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spSAD_UsrOrGrp_Permission", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCheckPermission(ByVal sAC As String, ByVal iACID As Integer, ByVal iModId As Integer, ByVal iUsrorGrpID As Integer, ByVal sPType As String)
        Dim sSql As String
        Try
            sSql = "Select SGP_View,SGP_New,SGP_SaveOrUpdate,SGP_Approve,SGP_ActivateOrDeactivate,SGP_Report,SGP_Annotaion,SGP_Download,SGP_Exception From SAD_UsrOrGrp_Permission Where SGP_CompID=" & iACID & " And  SGP_levelGroup = '" & sPType & "' and "
            sSql = sSql & "SGP_levelGroupID = " & iUsrorGrpID & " and SGP_modID = " & iModId & ""
            Return objDBL.SQLExecuteDataTable(sAC, sSql)

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetLoginUserPermission(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal sModCode As String) As String
        Dim sSql As String
        Dim iModuleID As Integer, iUserRoleLevel As Integer
        Try
            sSql = "" : sSql = "Select Mod_ID from SAD_MODULE where Mod_Code='" & sModCode & "'"
            iModuleID = objDBL.SQLExecuteScalarInt(sAC, sSql)
            'Check Is SuperUser
            If iModuleID = 0 Then
                Return ""
            Else
                sSql = "" : sSql = "Select Usr_ID from sad_userDetails where Usr_ID=" & iUserID & " and usr_LoginName='sa' and usr_FullName = 'System Admin' And Usr_CompID=" & iACID & ""
                'sSql = "" : sSql = "Select Usr_ID from sad_userDetails where Usr_ID=" & iUserID & " and Usr_IsPartner=1 And Usr_CompID=" & iACID & ""
                'Usr_ISSuperUser=1
                If objDBL.SQLCheckForRecord(sAC, sSql) = True Then
                    sSql = ",View,New,SaveOrUpdate,Approve,ActivateOrDeactivate,Report,"
                    Return sSql
                Else

                    '    sSql = "Select usr_usrgrpLvlPerm FRom Sad_UserDetails Where Usr_ID=" & iUserID & " and Usr_CompID=" & iACID & "" Dk
                    sSql = "Select Usr_GrpOrUserLvlPerm FRom Sad_UserDetails Where Usr_ID=" & iUserID & " and Usr_CompID=" & iACID & ""
                    iUserRoleLevel = objDBL.SQLExecuteScalarInt(sAC, sSql)

                    'Check User/Dept
                    If iUserRoleLevel = 0 Then
                        sSql = "" : sSql = "Select ',' + Case SGP_View When 1 then 'View' else '' end + ',' + Case SGP_New When 1 then 'New' else '' end + ',' + Case SGP_SaveOrUpdate When 1 then 'SaveOrUpdate' else '' end + ',' + Case SGP_Approve When 1 then 'Approve' else '' end + ',' + Case SGP_ActivateOrDeactivate When 1 then 'ActivateOrDeactivate' else '' end"
                        sSql = sSql & " + ',' + Case SGP_Report When 1 then 'Report,' else '' end + ',' + Case SGP_Download When 1 then 'Download' else '' end + ',' + Case SGP_Annotaion When 1 then 'Annotation' else '' end + ',' + Case  SGP_Exception When 1 then 'Exception' else '' end "
                        sSql = sSql & "  from Sad_UsrOrGrp_permission where sgp_Modid=" & iModuleID & " And SGP_LevelGroupID=" & iUserID & " And SGP_LevelGroup='R' and sgp_DelFlag='A' "
                    ElseIf iUserRoleLevel = 1 Then
                        sSql = "" : sSql = "Select ',' + Case SGP_View When 1 then 'View' else '' end + ',' + Case SGP_New When 1 then 'New' else '' end + ',' + Case SGP_SaveOrUpdate When 1 then 'SaveOrUpdate' else '' end + ',' + Case SGP_Approve When 1 then 'Approve' else '' end + ',' + Case SGP_ActivateOrDeactivate When 1 then 'ActivateOrDeactivate' else '' end"
                        sSql = sSql & " + ',' + Case SGP_Report When 1 then 'Report,' else '' end + ',' + Case SGP_Download When 1 then 'Download' else '' end + ',' + Case SGP_Annotaion When 1 then 'Annotation' else '' end + ',' + Case  SGP_Exception When 1 then 'Exception' else '' end "
                        sSql = sSql & " from Sad_UsrOrGrp_permission where sgp_Modid=" & iModuleID & " and sgp_DelFlag='A' And SGP_LevelGroup ='U' and"
                        '   sSql = sSql & "  SGP_LevelGroupID in (Select Usr_Designation From Sad_UserDetails Where Usr_ID=" & iUserID & " And Usr_CompID=" & iACID & ") "
                        sSql = sSql & "  SGP_LevelGroupID in (Select usr_Id From Sad_UserDetails Where Usr_ID=" & iUserID & " And Usr_CompID=" & iACID & ") "
                        'Usr_IsPartner     R->U
                    End If
                    '          sSql = "" : sSql = "Select Mas_desc from acc_General_Master where Mas_id In (Select usr_Designation FRom Sad_UserDetails Where Usr_ID=" & iUserID & " And Usr_CompID=" & iACID & ") "
                    '     If objDBL.SQLCheckForRecord(sAC, sSql) = True Then
                    '          sSql = ",View,New,SaveOrUpdate,Approve,ActivateOrDeactivate,Report,Exception"
                    '     Return sSql
                    ' End If
                    Return objDBL.SQLExecuteScalar(sAC, sSql)

                End If
            End If
            Return ""
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CopyDataNewCol(ByVal RefDt As DataTable) As DataTable
        Dim dc As DataColumn
        Try
            dc = New DataColumn("Mod_View", GetType(String))
            RefDt.Columns.Add(dc)
            dc = New DataColumn("Mod_New", GetType(String))
            RefDt.Columns.Add(dc)
            dc = New DataColumn("Mod_Save", GetType(String))
            RefDt.Columns.Add(dc)
            dc = New DataColumn("Mod_Active", GetType(String))
            RefDt.Columns.Add(dc)
            dc = New DataColumn("Mod_ActivateOrDeactivate", GetType(String))
            RefDt.Columns.Add(dc)
            dc = New DataColumn("Mod_Report", GetType(String))
            RefDt.Columns.Add(dc)
            For Each dr As DataRow In RefDt.Rows
                dr.BeginEdit()
                dr("Mod_View") = ""
                dr("Mod_New") = ""
                dr("Mod_Save") = ""
                dr("Mod_Active") = ""
                dr("Mod_ActivateOrDeactivate") = ""
                dr("Mod_Report") = ""
                dr.EndEdit()
                dr.AcceptChanges()
            Next
            Return RefDt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAccessRightsDetails(ByVal RefDt As DataTable, ByVal dtTable As DataTable) As DataTable
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim sView As String = "", sNew As String = "", sSave As String = "", sActive As String = "", sActivateOrDeactivate As String = "", sReport As String = ""
        Try
            dt.Columns.Add("Mod_Id")
            dt.Columns.Add("Mod_Description")
            dt.Columns.Add("mod_Function")
            dt.Columns.Add("Mod_View")
            dt.Columns.Add("Mod_New")
            dt.Columns.Add("Mod_Save")
            dt.Columns.Add("Mod_Active")
            dt.Columns.Add("Mod_ActivateOrDeactivate")
            dt.Columns.Add("Mod_Report")
            For i = 0 To RefDt.Rows.Count - 1
                dRow = dt.NewRow
                If IsDBNull(RefDt.Rows(i)("Mod_Id")) = False Then
                    dRow("Mod_Id") = RefDt.Rows(i)("Mod_Id")
                End If
                If IsDBNull(RefDt.Rows(i)("Mod_Description")) = False Then
                    dRow("Mod_Description") = RefDt.Rows(i)("Mod_Description")
                End If
                If IsDBNull(RefDt.Rows(i)("mod_Function")) = False Then
                    dRow("mod_Function") = RefDt.Rows(i)("mod_Function")
                End If
                Dim DVdtMaster As New DataView(dtTable)
                DVdtMaster.Sort = "SGP_modID"
                Dim sAppName As String = DVdtMaster.Find(dRow("Mod_Id"))
                If sAppName <> "-1" Then
                    sView = DVdtMaster(sAppName)("SGP_View")
                    dRow("Mod_View") = sView
                    sNew = DVdtMaster(sAppName)("SGP_New")
                    dRow("Mod_New") = sNew
                    sSave = DVdtMaster(sAppName)("SGP_SaveOrUpdate")
                    dRow("Mod_Save") = sSave
                    sActive = DVdtMaster(sAppName)("SGP_Approve")
                    dRow("Mod_Active") = sActive
                    sActivateOrDeactivate = DVdtMaster(sAppName)("SGP_ActivateOrDeactivate")
                    dRow("Mod_ActivateOrDeactivate") = sActivateOrDeactivate
                    sReport = DVdtMaster(sAppName)("SGP_Report")
                    dRow("Mod_Report") = sReport
                End If
                dt.Rows.Add(dRow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPermission(ByVal sAC As String, ByVal iACID As Integer, ByVal iModId As String, ByVal iUsrorGrpID As Integer, ByVal sPType As String)
        Dim sSql As String
        Dim dt As New DataTable, dtCol As New DataTable, dtdetails As New DataTable
        Dim dRow As DataRow
        Try
            dtCol.Columns.Add("SGP_modID")
            dtCol.Columns.Add("SGP_View")
            dtCol.Columns.Add("SGP_New")
            dtCol.Columns.Add("SGP_SaveOrUpdate")
            dtCol.Columns.Add("SGP_Approve")
            dtCol.Columns.Add("SGP_ActivateOrDeactivate")
            dtCol.Columns.Add("SGP_Report")

            sSql = "Select SGP_View,SGP_New,SGP_SaveOrUpdate,SGP_Approve,SGP_ActivateOrDeactivate,SGP_Report,SGP_modID From SAD_UsrOrGrp_Permission Where SGP_CompID=" & iACID & " And  SGP_levelGroup = '" & sPType & "' and "
            sSql = sSql & "SGP_levelGroupID = " & iUsrorGrpID & " and SGP_modID IN(" & iModId & ")"
            dtdetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtdetails.Rows.Count > 0 Then
                For i = 0 To dtdetails.Rows.Count - 1
                    dRow = dtCol.NewRow
                    If IsDBNull(dtdetails.Rows(i)("SGP_modID")) = False Then
                        dRow("SGP_modID") = dtdetails.Rows(i)("SGP_modID")
                    End If
                    If IsDBNull(dtdetails.Rows(i)("SGP_View")) = False Then
                        If dtdetails.Rows(i)("SGP_View") = 1 Then
                            dRow("SGP_View") = "Yes"
                        Else
                            dRow("SGP_View") = "No"
                        End If
                    End If
                    If IsDBNull(dtdetails.Rows(i)("SGP_New")) = False Then
                        If dtdetails.Rows(i)("SGP_New") = 1 Then
                            dRow("SGP_New") = "Yes"
                        Else
                            dRow("SGP_New") = "No"
                        End If
                    End If
                    If IsDBNull(dtdetails.Rows(i)("SGP_SaveOrUpdate")) = False Then
                        If dtdetails.Rows(i)("SGP_SaveOrUpdate") = 1 Then
                            dRow("SGP_SaveOrUpdate") = "Yes"
                        Else
                            dRow("SGP_SaveOrUpdate") = "No"
                        End If
                    End If
                    If IsDBNull(dtdetails.Rows(i)("SGP_Approve")) = False Then
                        If dtdetails.Rows(i)("SGP_Approve") = 1 Then
                            dRow("SGP_Approve") = "Yes"
                        Else
                            dRow("SGP_Approve") = "No"
                        End If
                    End If
                    If IsDBNull(dtdetails.Rows(i)("SGP_ActivateOrDeactivate")) = False Then
                        If dtdetails.Rows(i)("SGP_ActivateOrDeactivate") = 1 Then
                            dRow("SGP_ActivateOrDeactivate") = "Yes"
                        Else
                            dRow("SGP_ActivateOrDeactivate") = "No"
                        End If
                    End If
                    If IsDBNull(dtdetails.Rows(i)("SGP_Report")) = False Then
                        If dtdetails.Rows(i)("SGP_Report") = 1 Then
                            dRow("SGP_Report") = "Yes"
                        Else
                            dRow("SGP_Report") = "No"
                        End If
                    End If
                    dtCol.Rows.Add(dRow)
                Next
            End If
            Return dtCol
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function getCurrentUserRole(ByVal iCurrentUsrId As Integer, ByVal sAC As String, ByVal iACID As Integer) As String
        Dim ssql As String
        Dim sRole As String
        Try
            ssql = "" : ssql = "Select Mas_desc from acc_General_Master where Mas_id In (Select usr_Role FRom Sad_UserDetails Where Usr_ID=" & iCurrentUsrId & " And Usr_CompID=" & iACID & ") "
            sRole = objDBL.SQLExecuteScalar(sAC, ssql)
            Return sRole
        Catch ex As Exception
        End Try
    End Function
End Class
