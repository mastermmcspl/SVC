Imports System
Imports System.Data
Imports BusinesLayer
Imports System.IO
Partial Class DigitalFilling_UploadToServer
    Inherits System.Web.UI.Page
    Private sFormName As String = "DigitalFilling_UploadToServer"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsFASGeneral As New clsFASGeneral

    Private Shared sSession As AllSession
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim strImageFolderName As String, sSessionScanPath As String, sScanFileSavePath As String
        Dim strImageDetails As String(), AllFiles() As String
        Try
            sSession = Session("AllSession")
            Dim files As HttpFileCollection = HttpContext.Current.Request.Files
            Dim uploadfile As HttpPostedFile = files("RemoteFile")
            strImageFolderName = uploadfile.FileName
            If strImageFolderName.Contains("#") = True Then
                strImageDetails = strImageFolderName.Split("#")

                sSessionScanPath = Server.MapPath(".") 'objclsGeneralFunctions.CheckAndCreateWorkingDirFromPath(sSession.ScanPath)
                If Request.QueryString("Delete") = "YES" Then
                    sScanFileSavePath = sSessionScanPath & "\\NormalScan\\" & strImageDetails(0)
                Else
                    sScanFileSavePath = sSessionScanPath & "\\NormalScan\\" & strImageDetails(0) & "_" & sSession.UserID.ToString()
                End If


                If Request.QueryString("Delete") IsNot Nothing And Request.QueryString("ForLoop") IsNot Nothing Then
                    If Request.QueryString("Delete") = "YES" And Request.QueryString("ForLoop") = "0" Then
                        AllFiles = Directory.GetFileSystemEntries(sScanFileSavePath)
                        For Each element As String In AllFiles
                            If System.IO.File.Exists(element) = True Then
                                Try
                                    My.Computer.FileSystem.DeleteFile(element)
                                Catch ex As Exception
                                End Try
                            End If
                        Next
                    End If
                End If

                objclsGeneralFunctions.CheckAndCreateWorkingDirFromPath(sSession.AccessCode, sScanFileSavePath)

                uploadfile.SaveAs(sScanFileSavePath + "\\" + sSession.UserID.ToString() + "_" + strImageDetails(1))
            End If
        Catch
        End Try
    End Sub
End Class
