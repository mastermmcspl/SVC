Imports System
Imports System.IO
Imports System.Data
Imports BusinesLayer
Partial Class DigitalFilling_NormalScanDetails
    Inherits System.Web.UI.Page
    Private sFormName As String = "DigitalFilling_NormalScanDetails"
    Private objErrorClass As New Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACeGeneral As New clsFASGeneral
    Private Shared sSession As AllSession
    Private Shared sFolder As String
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)

        Session("TypeOfImage") = "RGB"
        Session("ImageFormat") = "jpg"
        Session("Resolution") = "150"
        Session("AutoFeeder") = "NO"
        Session("ShowUI") = "NO"

        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                If Request.QueryString("Folder") IsNot Nothing Then
                    sFolder = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("Folder")))
                    BindImages("\NormalScan\" & sFolder)
                    lblFolderName.Text = sFolder
                End If
            End If
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub BindImages(ByVal sFolderPath As String)
        Dim dt As New DataTable
        Dim drow As DataRow
        Dim i As Integer = 0
        Try
            dt.Columns.Add("No")
            dt.Columns.Add("Path")

            Dim files() As String = Directory.GetFileSystemEntries(Server.MapPath(".") & sFolderPath)
            For Each element As String In files
                If System.IO.File.Exists(element) = True Then
                    Try
                        i = i + 1
                        drow = dt.NewRow
                        drow("No") = i
                        drow("Path") = sFolderPath & "\" & System.IO.Path.GetFileName(element)
                        dt.Rows.Add(drow)
                    Catch ex As Exception
                    End Try
                End If
            Next

            lstImages.DataSource = dt
            lstImages.DataTextField = "Path"
            lstImages.DataValueField = "No"
            lstImages.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub imgbtnDelete_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnDelete.Click
        Try
            For Each item As ListItem In lstImages.Items
                If item.Selected = True Then
                    Dim sImagepath As String = Server.MapPath(".") & item.Text.ToString()
                    If System.IO.File.Exists(sImagepath) = True Then
                        Try
                            My.Computer.FileSystem.DeleteFile(sImagepath)
                        Catch ex As Exception
                        End Try
                    End If
                End If
            Next
            BindImages("\NormalScan\" & sFolder)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnDelete_Click")
        End Try
    End Sub
    Private Sub imgbtnDeleteAll_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnDeleteAll.Click
        Try
            For Each item As ListItem In lstImages.Items
                If System.IO.File.Exists(Server.MapPath(".") & item.Text.ToString()) = True Then
                    Dim sImagepath As String = Server.MapPath(".") & item.Text.ToString()
                    If System.IO.File.Exists(sImagepath) = True Then
                        Try
                            My.Computer.FileSystem.DeleteFile(sImagepath)
                        Catch ex As Exception
                        End Try
                    End If
                End If
            Next
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnDeleteAll_Click")
        End Try
    End Sub
End Class

