Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class ClsPrintSettings
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral
    Public Function getImageName(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sStr As String) As String
        Dim sSql As String = ""
        Dim sImageName As String = ""
        Dim dt As New DataTable
        Try
            sSql = "SELECT (PS_FileName + '.' + PS_Extn) As PS_FileName FROM Print_Settings WHERE PS_Status='" & sStr & "'"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If IsDBNull(dt.Rows(0)("PS_FileName")) = False Then
                sImageName = dt.Rows(0)("PS_FileName")
                If sImageName = "NULL.NULL" Then
                    sImageName = ""
                End If
            Else
                sImageName = ""
            End If
            Return sImageName
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
