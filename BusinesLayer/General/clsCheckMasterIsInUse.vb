Imports System
Imports System.Data
Imports System.IO
Imports DatabaseLayer
Public Class clsCheckMasterIsInUse
    Private objDBL As New DatabaseLayer.DBHelper
    Public Function CheckOrganizationStructureIsInUse(ByVal sAC As String, ByVal iACID As Integer, ByVal iOrgnID As Integer) As Boolean
        Dim sSql As String = ""
        Try
            'Organization Structure
            sSql = "Select Usr_ID From Sad_userDetails Where Usr_OrgnID=" & iOrgnID & " and Usr_CompID=" & iACID & ""
            If objDBL.SQLCheckForRecord(sAC, sSql) = True Then
                Return True
            End If

            '----------------Both tables not exists in DataBase--------------------------
            'BRR Planning
            'sSql = "Select BRRP_PKID from Risk_BRRPlanning Where (BRRP_BranchID=" & iOrgnID & "  OR BRRP_RegionID=" & iOrgnID & " OR BRRP_ZOneID=" & iOrgnID & ") and BRRP_CompID=" & iACID & ""
            'If objDBL.SQLCheckForRecord(sAC, sSql) = True Then
            '    Return True
            'End If

            'KIR
            'sSql = "Select KIR_PKID from Risk_KIR Where (KIR_Zone=" & iOrgnID & " OR KIR_Region=" & iOrgnID & ") And KIR_CompID=" & iACID & ""
            'If objDBL.SQLCheckForRecord(sAC, sSql) = True Then
            '    Return True
            'End If
            'Return False
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckEmployeeNameIsInUse(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer) As Boolean
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            'Function Owner
            sSql = "Select ENT_ID from MST_Entity_Master Where Ent_FunOwnerID=" & iUserID & " And ENT_Compid=" & iACID & ""
            If objDBL.SQLCheckForRecord(sAC, sSql) = True Then
                Return True
            End If

            'Function Manager
            sSql = "Select ENT_ID from MST_Entity_Master Where Ent_FunManagerID=" & iUserID & " And ENT_Compid=" & iACID & ""
            If objDBL.SQLCheckForRecord(sAC, sSql) = True Then
                Return True
            End If

            'Function SPOC
            sSql = "Select ENT_ID from MST_Entity_Master Where Ent_FunSPOCID=" & iUserID & " And ENT_Compid=" & iACID & ""
            If objDBL.SQLCheckForRecord(sAC, sSql) = True Then
                Return True
            End If

            'Risk Issue Tracker
            sSql = "Select RIT_PKID from Risk_IssueTracker Where RIT_IndividualResponsible=" & iUserID & " And RIT_CompID=" & iACID & ""
            If objDBL.SQLCheckForRecord(sAC, sSql) = True Then
                Return True
            End If

            'KCC Planning
            sSql = "Select KCC_PKID from Risk_KCC_PlanningSchecduling_Details  Where KCC_ReviewerID=" & iUserID & " And KCC_CompID=" & iACID & ""
            If objDBL.SQLCheckForRecord(sAC, sSql) = True Then
                Return True
            End If

            'BRR Scheduling
            sSql = "Select BRRS_PKID from Risk_BRRSchedule Where (BRRS_BranchMgrID=" & iUserID & " OR BRRS_EmployeeID=" & iUserID & "  OR BRRS_ZonalMgrID=" & iUserID & ") And BRRS_CompID=" & iACID & ""
            If objDBL.SQLCheckForRecord(sAC, sSql) = True Then
                Return True
            End If

            'KIR
            sSql = "Select KIR_PKID from Risk_KIR Where KIR_EmpCode=" & iUserID & " And KIR_CompID=" & iACID & ""
            If objDBL.SQLCheckForRecord(sAC, sSql) = True Then
                Return True
            End If

            'Functional APM
            sSql = "Select APM_ID from Audit_APM_Details Where (APM_FunctionHODID=" & iUserID & " Or APM_FunctionSPOCID=" & iUserID & " Or APM_EmailFromID like '%," & iUserID & ",%') And APM_CompID=" & iACID & ""
            If objDBL.SQLCheckForRecord(sAC, sSql) = True Then
                Return True
            End If

            ''Functional APM EmailTO,EmailCC,TeamMembers
            'sSql = "Select APM_EmailTOID,APM_EmailCCID,APM_TeamMembersID from Audit_APM_Details Where APM_CompID=" & iACID & ""
            'dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            'For i = 0 To dt.Rows.Count - 1
            '    If IsDBNull(dt.Rows(i)("APM_EmailTOID")) = False Then
            '        If dt.Rows(i)("APM_EmailTOID") <> "" Then
            '            EmailID = dt.Rows(i)("APM_EmailTOID")
            '            EmailAPM = EmailID.Split(",")
            '            For j = 1 To EmailAPM.Length - 2
            '                If EmailAPM.Length > 1 Then
            '                    Email = objDBL.SQLExecuteScalar(sAC, "Select usr_Id from Sad_UserDetails where usr_Id=" & EmailAPM(j) & " and Usr_CompId= " & iACID & "")
            '                    sSql = "Select APM_ID from Audit_APM_Details Where APM_EmailTOID like '%," & Email & ",%' And APM_CompID=" & iACID & ""
            '                    If objDBL.SQLCheckForRecord(sAC, sSql) = True Then
            '                        Return True
            '                    End If
            '                End If
            '            Next
            '        End If
            '    End If
            'Next
            'For i = 0 To dt.Rows.Count - 1
            '    If IsDBNull(dt.Rows(i)("APM_EmailCCID")) = False Then
            '        If dt.Rows(i)("APM_EmailCCID") <> "" Then
            '            EmailID = dt.Rows(i)("APM_EmailCCID")
            '            EmailAPM = EmailID.Split(",")
            '            For j = 1 To EmailAPM.Length - 2
            '                If EmailAPM.Length > 1 Then
            '                    Email = objDBL.SQLExecuteScalar(sAC, "Select usr_Id from Sad_UserDetails where usr_Id=" & EmailAPM(j) & " and Usr_CompId= " & iACID & "")
            '                    sSql = "Select APM_ID from Audit_APM_Details Where APM_EmailCCID like '%," & Email & ",%' And APM_CompID=" & iACID & ""
            '                    If objDBL.SQLCheckForRecord(sAC, sSql) = True Then
            '                        Return True
            '                    End If
            '                End If
            '            Next
            '        End If
            '    End If
            'Next
            'For i = 0 To dt.Rows.Count - 1
            '    If IsDBNull(dt.Rows(i)("APM_TeamMembersID")) = False Then
            '        If dt.Rows(i)("APM_TeamMembersID") <> "" Then
            '            EmailID = dt.Rows(i)("APM_TeamMembersID")
            '            EmailAPM = EmailID.Split(",")
            '            For j = 1 To EmailAPM.Length - 2
            '                If EmailAPM.Length > 1 Then
            '                    Email = objDBL.SQLExecuteScalar(sAC, "Select usr_Id from Sad_UserDetails where usr_Id=" & EmailAPM(j) & " and Usr_CompId= " & iACID & "")
            '                    sSql = "Select APM_ID from Audit_APM_Details Where APM_TeamMembersID=" & Email & " And APM_CompID=" & iACID & ""
            '                    If objDBL.SQLCheckForRecord(sAC, sSql) = True Then
            '                        Return True
            '                    End If
            '                End If
            '            Next
            '        End If
            '    End If
            'Next

            'Functional Issue Tracker
            sSql = "Select AIT_PKID from Audit_IssueTracker_details Where AIT_FunctionManagerID=" & iUserID & " and AIT_CompID=" & iACID & ""
            If objDBL.SQLCheckForRecord(sAC, sSql) = True Then
                Return True
            End If

            'BCM Assignment
            sSql = "Select CAB_ID from CMA_Assignment_Branches Where CAB_CarManager=" & iUserID & " And CAB_CompId=" & iACID & ""
            If objDBL.SQLCheckForRecord(sAC, sSql) = True Then
                Return True
            End If

            sSql = "Select CVA_ID from CMA_Vendor_Assignment Where (CVA_VendorID=" & iUserID & " Or CVA_BranchManager=" & iUserID & ") And CVA_CompId=" & iACID & ""
            If objDBL.SQLCheckForRecord(sAC, sSql) = True Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function CheckPurchaseOrderInUse(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer) As Boolean
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            'Purchase Gin 
            sSql = "Select PGM_ID from purchase_gin_master Where PGM_OrderID=" & iUserID & " And PGM_Compid=" & iACID & ""
            If objDBL.SQLCheckForRecord(sAC, sSql) = True Then
                Return True
            End If

            Return False
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
