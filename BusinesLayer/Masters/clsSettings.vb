Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class clsSettings
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral
    Dim objGnrl As New clsGeneralFunctions
    Public Function LoadGroup(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iHead As Integer) As DataSet
        Dim sSql As String = ""
        Dim ds As New DataSet
        Try
            If iHead = 0 Then
                sSql = "" : sSql = "Select gl_id, (gl_glCode + ' - ' + gl_desc) as Description from Chart_Of_Accounts where gl_head = 0 and "
                sSql = sSql & " gl_CompId =" & iCompID & " and gl_Status ='A' order by gl_id"
            Else
                sSql = "" : sSql = "Select gl_id, (gl_glCode + ' - ' + gl_desc) as Description from Chart_Of_Accounts where gl_head = 0 and "
                sSql = sSql & "gl_AccHead =" & iHead & "  and gl_CompId =" & iCompID & " and gl_Status ='A' order by gl_id"
            End If

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
    Public Function LoadGL(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iSubGroup As Integer) As DataSet
        Dim sSql As String = ""
        Dim ds As New DataSet
        Try
            sSql = "" : sSql = "Select gl_id, (gl_glCode + ' - ' + gl_desc) as Description from Chart_Of_Accounts where gl_head = 2 and "
            sSql = sSql & "gl_Parent =" & iSubGroup & " and gl_Status ='A' and gl_CompId =" & iCompID & " order by gl_id"
            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            Return ds
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function UpdateSettings(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iGroup As Integer, ByVal iSubGroup As Integer, ByVal iGL As Integer, ByVal iSubGL As Integer, ByVal sTypes As String, ByVal sLedgerType As String)
        Dim sSql As String = ""
        Dim iHeadID As Integer = 0
        Try
            iHeadID = objDBL.SQLExecuteScalar(sNameSpace, "Select gl_AccHead from chart_of_Accounts where gl_id =" & iGroup & " and gl_CompID =" & iCompID & "")
            sSql = "" : sSql = "Update Acc_Application_Settings set Acc_Head = " & iHeadID & ",Acc_Group=" & iGroup & ","
            sSql = sSql & "Acc_SubGroup = " & iSubGroup & ",Acc_GL=" & iGL & ",Acc_SubGL=" & iSubGL & " where Acc_Types ='" & sTypes & "' and "
            sSql = sSql & "Acc_LedgerType = '" & sLedgerType & "'"
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSubGL(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iGL As Integer) As DataSet
        Dim sSql As String = ""
        Dim ds As New DataSet
        Try
            sSql = "" : sSql = "Select gl_id, (gl_glCode + ' - ' + gl_desc) as Description from Chart_Of_Accounts where gl_head = 3 and "
            sSql = sSql & "gl_Parent =" & iGL & " and gl_Status ='A' And gl_CompId =" & iCompID & " order by gl_id"
            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
            Return ds
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sType As String, ByVal sLedgerType As String) As String
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim sPerm As String = ""
        Try
            sSql = "" : sSql = "Select * from acc_Application_Settings where Acc_Types = '" & sType & "' and Acc_LedgerType = '" & sLedgerType & "' and Acc_CompID = " & iCompID & ""
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                If dt.Rows(0)("Acc_Group").ToString() <> "" Then
                    sPerm = sPerm & "," & dt.Rows(0)("Acc_Group").ToString()
                Else
                    sPerm = sPerm & "," & "0"
                End If

                If dt.Rows(0)("Acc_SubGroup").ToString() <> "" Then
                    sPerm = sPerm & "," & dt.Rows(0)("Acc_SubGroup").ToString()
                Else
                    sPerm = sPerm & "," & "0"
                End If

                If dt.Rows(0)("Acc_GL").ToString() <> "" Then
                    sPerm = sPerm & "," & dt.Rows(0)("Acc_GL").ToString()
                Else
                    sPerm = sPerm & "," & "0"
                End If

                If dt.Rows(0)("Acc_SubGL").ToString() <> "" Then
                    sPerm = sPerm & "," & dt.Rows(0)("Acc_SubGL").ToString()
                Else
                    sPerm = sPerm & "," & "0"
                End If

                Return sPerm
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDetailsSetttings(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sType As String, ByVal sLedgerType As String) As String
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim sPerm As String = ""
        Try
            sSql = "" : sSql = "Select * from acc_Application_Settings where Acc_Types = '" & sType & "' and Acc_LedgerType = '" & sLedgerType & "' and Acc_CompID = " & iCompID & ""
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dt.Rows.Count > 0 Then
                If dt.Rows(0)("Acc_Head").ToString() <> "" Then
                    sPerm = sPerm & "," & dt.Rows(0)("Acc_Head").ToString()
                Else
                    sPerm = sPerm & "," & "0"
                End If

                If dt.Rows(0)("Acc_Group").ToString() <> "" Then
                    sPerm = sPerm & "," & dt.Rows(0)("Acc_Group").ToString()
                Else
                    sPerm = sPerm & "," & "0"
                End If

                If dt.Rows(0)("Acc_SubGroup").ToString() <> "" Then
                    sPerm = sPerm & "," & dt.Rows(0)("Acc_SubGroup").ToString()
                Else
                    sPerm = sPerm & "," & "0"
                End If

                If dt.Rows(0)("Acc_GL").ToString() <> "" Then
                    sPerm = sPerm & "," & dt.Rows(0)("Acc_GL").ToString()
                Else
                    sPerm = sPerm & "," & "0"
                End If

                If dt.Rows(0)("Acc_SubGL").ToString() <> "" Then
                    sPerm = sPerm & "," & dt.Rows(0)("Acc_SubGL").ToString()
                Else
                    sPerm = sPerm & "," & "0"
                End If

                Return sPerm
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindGSTRates(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "SElect Distinct(GST_GSTRate) From GST_Rates Where GST_CompID=" & iCompID & " "
            BindGSTRates = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return BindGSTRates
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveSettings(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iGroup As Integer, ByVal iSubGroup As Integer, ByVal iGL As Integer, ByVal iSubGL As Integer, ByVal sTypes As String, ByVal sLedgerType As String, ByVal sTypeOfBill As String)
        Dim sSql As String = ""
        Dim iHeadID As Integer = 0
        Dim iMax As Integer
        Dim bCheck As Boolean
        Try
            bCheck = objDBL.SQLCheckForRecord(sNameSpace, "Select * From Acc_Application_Settings Where Acc_Types='" & sTypes & "' And Acc_LedgerType='" & sLedgerType & "' And Acc_TypeOfBill='" & sTypeOfBill & "' And Acc_CompID=" & iCompID & " ")
            If bCheck = True Then

            Else
                iHeadID = objDBL.SQLExecuteScalar(sNameSpace, "Select gl_AccHead from chart_of_Accounts where gl_id =" & iGroup & " and gl_CompID =" & iCompID & "")
                iMax = objGnrl.GetMaxID(sNameSpace, iCompID, "Acc_Application_Settings", "Acc_ID", "Acc_CompID")
                sSql = "" : sSql = "Insert Into Acc_Application_Settings(Acc_ID,Acc_Types,Acc_LedgerType,Acc_Head,Acc_Group,Acc_SubGroup,Acc_GL,Acc_SubGL,Acc_CompID,Acc_TypeOfBill) "
                sSql = sSql & " Values(" & iMax & ",'" & sTypes & "','" & sLedgerType & "'," & iHeadID & "," & iGroup & "," & iSubGroup & "," & iGL & "," & iSubGL & "," & iCompID & ",'" & sTypeOfBill & "')"
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadPurchaseDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim dt As New DataTable
        Dim ds As New DataSet
        Dim dc As New DataColumn
        Dim sSql As String = "", aSql As String = ""
        Dim dr As DataRow
        Dim i As Integer = 0
        Try
            dc = New DataColumn("Id", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("HeadID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("GLID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("SubGLID", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Type", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("GLCode", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("GLDescription", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("SubGL", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("SubGLDescription", GetType(String))
            dt.Columns.Add(dc)

            sSql = "Select * from Acc_Application_Settings where (Acc_Types <> 'Customer' And ACC_Types<>'Supplier' And Acc_Types<>'Cash' And Acc_Types<>'Bank') And Acc_CompID=" & iCompID & " order by Acc_ID "
            ds = objDBL.SQLExecuteDataSet(sNameSpace, sSql)

            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dr = dt.NewRow

                    If IsDBNull(ds.Tables(0).Rows(i)("ACC_ID").ToString()) = False Then
                        dr("Id") = ds.Tables(0).Rows(i)("ACC_ID").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("ACC_Head").ToString()) = False Then
                        dr("HeadID") = ds.Tables(0).Rows(i)("ACC_Head").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("ACC_GL").ToString()) = False Then
                        dr("GLID") = ds.Tables(0).Rows(i)("ACC_GL").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("ACC_SubGL").ToString()) = False Then
                        dr("SubGLID") = ds.Tables(0).Rows(i)("ACC_SubGL").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("ACC_Types").ToString()) = False Then
                        dr("Type") = ds.Tables(0).Rows(i)("ACC_Types").ToString()
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("ACC_GL").ToString()) = False Then
                        If ds.Tables(0).Rows(i)("ACC_GL") > 0 Then
                            dr("GLCode") = objDBL.SQLGetDescription(sNameSpace, "Select gl_glCode From Chart_Of_Accounts Where gl_id=" & ds.Tables(0).Rows(i)("ACC_GL") & " ")
                        Else
                            dr("GLCode") = ""
                        End If
                    Else
                        dr("GLCode") = ""
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("ACC_GL").ToString()) = False Then
                        If ds.Tables(0).Rows(i)("ACC_GL") > 0 Then
                            dr("GLDescription") = objDBL.SQLGetDescription(sNameSpace, "Select gl_Desc From Chart_Of_Accounts Where gl_id=" & ds.Tables(0).Rows(i)("ACC_GL") & " ")
                        Else
                            dr("GLDescription") = ""
                        End If
                    Else
                        dr("GLDescription") = ""
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("ACC_SubGL").ToString()) = False Then
                        If ds.Tables(0).Rows(i)("ACC_SubGL") > 0 Then
                            dr("SubGL") = objDBL.SQLGetDescription(sNameSpace, "Select gl_glCode From Chart_Of_Accounts Where gl_id=" & ds.Tables(0).Rows(i)("ACC_SubGL") & " ")
                        Else
                            dr("SubGL") = ""
                        End If
                    Else
                        dr("SubGL") = ""
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i)("ACC_SubGL").ToString()) = False Then
                        If ds.Tables(0).Rows(i)("ACC_SubGL") > 0 Then
                            dr("SubGLDescription") = objDBL.SQLGetDescription(sNameSpace, "Select gl_Desc From Chart_Of_Accounts Where gl_id=" & ds.Tables(0).Rows(i)("ACC_SubGL") & " ")
                        Else
                            dr("SubGLDescription") = ""
                        End If
                    Else
                        dr("SubGLDescription") = ""
                    End If

                    dt.Rows.Add(dr)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
