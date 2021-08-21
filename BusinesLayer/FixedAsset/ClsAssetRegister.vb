Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class ClsAssetRegister
    Private objDBL As New DatabaseLayer.DBHelper
    Public objFAS As New clsFASGeneral

    Public Function LoadAssetRegister(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAFAM_AssetType As Integer, ByVal iYearId As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt, dt1 As New DataTable
        Dim dr As DataRow
        Try
            dt.Columns.Add("ID")
            dt.Columns.Add("AssetID")
            dt.Columns.Add("AssetCode")
            dt.Columns.Add("AssetDescription")
            dt.Columns.Add("PurchaseDate")
            dt.Columns.Add("PurchaseAmount")
            dt.Columns.Add("Datecommission")
            dt.Columns.Add("AssetAge")
            dt.Columns.Add("Qty")
            dt.Columns.Add("DepMethod")
            dt.Columns.Add("DepRate")
            dt.Columns.Add("CurrentStatus")

            sSql = "Select * From Acc_FixedAssetMaster Where AFAM_CompID=" & iCompID & " And AFAM_AssetType=" & iAFAM_AssetType & " and AFAM_YearID='" & iYearId & "' order by AFAM_ID   "
            dt1 = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)

            If dt1.Rows.Count > 0 Then
                For i = 0 To dt1.Rows.Count - 1
                    dr = dt.NewRow
                    If IsDBNull(dt1.Rows(i)("AFAM_ID")) = False Then
                        dr("ID") = dt1.Rows(i)("AFAM_ID")
                    Else
                        dr("ID") = 0
                    End If
                    If IsDBNull(dt1.Rows(i)("AFAM_AssetType")) = False Then
                        dr("AssetID") = dt1.Rows(i)("AFAM_AssetType")
                    Else
                        dr("AssetID") = 0
                    End If
                    If IsDBNull(dt1.Rows(i)("AFAM_AssetCode")) = False Then
                        dr("AssetCode") = dt1.Rows(i)("AFAM_AssetCode")
                    Else
                        dr("AssetCode") = 0
                    End If
                    If IsDBNull(dt1.Rows(i)("AFAM_Description")) = False Then
                        dr("AssetDescription") = dt1.Rows(i)("AFAM_Description")
                    Else
                        dr("AssetDescription") = ""
                    End If

                    If IsDBNull(dt1.Rows(i)("AFAM_PurchaseDate")) = False Then
                        dr("PurchaseDate") = objFAS.FormatDtForRDBMS(dt1.Rows(i)("AFAM_PurchaseDate"), "D")
                    Else
                        dr("PurchaseDate") = ""
                    End If
                    If IsDBNull(dt1.Rows(i)("AFAM_PurchaseAmount")) = False Then
                        dr("PurchaseAmount") = dt1.Rows(i)("AFAM_PurchaseAmount")
                    Else
                        dr("PurchaseAmount") = 0
                    End If

                    If IsDBNull(dt1.Rows(i)("AFAM_CommissionDate")) = False Then
                        dr("Datecommission") = objFAS.FormatDtForRDBMS(dt1.Rows(i)("AFAM_CommissionDate"), "D")
                    Else
                        dr("Datecommission") = ""
                    End If

                    If IsDBNull(dt1.Rows(i)("AFAM_AssetAge")) = False Then
                        dr("AssetAge") = dt1.Rows(i)("AFAM_AssetAge")
                    Else
                        dr("AssetAge") = 0
                    End If
                    If IsDBNull(dt1.Rows(i)("AFAM_Quantity")) = False Then
                        dr("Qty") = dt1.Rows(i)("AFAM_Quantity")
                    Else
                        dr("Qty") = 0
                    End If

                    If IsDBNull(dt1.Rows(i)("AFAM_Status")) = False Then
                        If dt1.Rows(i)("AFAM_Status") = "W" Then
                            dr("CurrentStatus") = "Waiting for Approval"
                        ElseIf dt1.Rows(i)("AFAM_Status") = "A" Then
                            dr("CurrentStatus") = "Approved"
                        End If
                    End If

                    Dim res As Integer
                    res = objDBL.SQLExecuteScalar(sNameSpace, "select AS_DepMethod from  Application_Settings where AS_CompID=" & iCompID & "")
                    If res = 1 Then
                        dr("DepMethod") = "SLM"
                    Else
                        dr("DepMethod") = "WDV"
                    End If

                    'dr("DepRate") = objDBL.SQLExecuteScalar(sNameSpace, "select Mas_DepRate from ACC_General_Master where Mas_CompID=" & iCompID & " and Mas_master in (Select Mas_ID From Acc_Master_Type Where Mas_Type='Asset Type') and Mas_Id=" & iAFAM_AssetType & "")

                    dr("DepRate") = objDBL.SQLExecuteScalar(sNameSpace, "select AM_Deprate from Acc_AssetMaster where AM_CompID=" & iCompID & " and AM_AssetID=" & iAFAM_AssetType & "")

                    dt.Rows.Add(dr)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function loadAssetType(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select GL_Desc,GL_ID From Chart_Of_Accounts Where GL_Parent In (Select GL_ID From Chart_Of_Accounts Where GL_Parent In (Select gl_ID From Chart_Of_Accounts Where GL_Desc='Fixed assets'))"
            'sSql = "Select * From Acc_General_Master Where Mas_CompID='" & iCompID & "' and Mas_Master in (Select Mas_ID From Acc_Master_Type Where Mas_Type='Asset Type') "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt, dt1 As New DataTable
        Try
            sSql = "Select * From Acc_FixedAssetMaster Where AFAM_ID=" & iID & " And AFAM_CompID=" & iCompID & "  "
            dt1 = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt1
        Catch ex As Exception
            Throw
        End Try
    End Function

End Class
