Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class ClsSalesPartyUpload
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions
    Public Sub SaveSalesPartyData(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iUserID As Integer, ByVal iYear As Integer, ByVal sCode As String, ByVal sName As String, ByVal sAddress As String, ByVal sAddress1 As String, ByVal sAddress2 As String, ByVal sAddress3 As String, ByVal sPinCode As String, ByVal sContact As String, ByVal sOffice As String, ByVal sMobile As String, ByVal sMail As String, ByVal city As String, ByVal State As String, ByVal FaxNo As String, ByVal TINNo As String, ByVal sIPAddress As String, ByVal iGroup As Integer, ByVal iSubGroup As Integer, ByVal iGL As Integer, ByVal iSubGL As Integer)
        Dim ssql As String
        Dim iBM_Id As Integer
        Dim iBMId As Integer
        'Dim sCode As String = ""
        Dim sDescCode As String = ""
        Dim iParent As Integer
        Dim sSubGrp As String = ""

        Dim iBuyerExistingID, iBuyerPKID As Integer
        Try
            iBM_Id = objDBL.SQLExecuteScalarInt(sNameSpace, "select BM_Id from Sales_Buyers_Masters where BM_Code='" & sCode & "' and BM_CompId=" & iCompID & " ")
            If iBM_Id = 0 Then
                iBMId = objDBL.SQLExecuteScalar(sNameSpace, "Select  IsNull(MAX(BM_ID),0)+1 from Sales_Buyers_Masters")
                'sCode = "C-" & "" & iBMId & ""
                'ssql = "Insert into Sales_Buyers_Masters(BM_ID,BM_IndType,BM_CustType,BM_Name,BM_Code,BM_Under,BM_Inventry,BM_MailName,BM_Address,BM_State,BM_Pincode,BM_PAN,BM_TIN,BM_Delflag,BM_CompID,BM_CreatedBy,BM_CreatedOn,BM_ApprovedBy,BM_ApprovedOn,BM_DeletedBy,BM_DeletedOn,BM_Status,BM_UpdatedBy,BM_UpdatedOn,BM_Contact,BM_City,BM_Country,BM_Office,BM_MobileNo,BM_Email,BM_Fax,BM_Year)"
                ssql = "" : ssql = "Insert into Sales_Buyers_Masters(BM_ID,BM_Name,BM_Code,BM_Address,BM_Pincode,BM_Delflag,BM_CompID,BM_CreatedBy,BM_CreatedOn,BM_Status,BM_ContactPerson,BM_LandLineNo,BM_MobileNo,BM_EmailID,BM_Year,BM_City,BM_State,BM_Fax,BM_IPAddress,BM_Address1,BM_Address2,BM_Address3,BM_Group,BM_SubGroup,BM_GL,BM_SubGL)"
                ssql = ssql & "values(" & iBMId & ",'" & sName & "','" & sCode & "','" & sAddress & "','" & sPinCode & "','A'," & iCompID & "," & iUserID & ",GetDate(),'A','" & sContact & "','" & sOffice & "','" & sMobile & "','" & sMail & "'," & iYear & ",'" & city & "','" & State & "','" & FaxNo & "','" & sIPAddress & "','" & sAddress1 & "','" & sAddress2 & "','" & sAddress3 & "'," & iGroup & "," & iSubGroup & "," & iGL & "," & iSubGL & ")"
                objDBL.SQLExecuteNonQuery(sNameSpace, ssql)
                iBM_Id = iBMId
            Else
                ssql = "" : ssql = "Update Sales_Buyers_Masters set BM_Name='" & sName & "',BM_Address='" & sAddress & "',BM_Address1='" & sAddress1 & "',BM_Address2='" & sAddress2 & "',BM_Address3='" & sAddress3 & "',BM_Pincode='" & sPinCode & "',BM_ContactPerson='" & sContact & "',BM_LandLineNo='" & sOffice & "',BM_MobileNo='" & sMobile & "',BM_EmailID='" & sMail & "',BM_City= '" & city & "',BM_State='" & State & "',BM_Fax='" & FaxNo & "',BM_UpdatedBy=" & iUserID & ",BM_UpdatedOn=GetDate(),BM_Group=" & iGroup & ",BM_SubGroup=" & iSubGroup & ",BM_GL=" & iGL & ",BM_SubGL=" & iSubGL & " where BM_Code='" & sCode & "' And BM_Id=" & iBM_Id & " and BM_CompID=" & iCompID & " and BM_Year=" & iYear & " "
                objDBL.SQLExecuteNonQuery(sNameSpace, ssql)
            End If

            iBuyerExistingID = objDBL.SQLExecuteScalarInt(sNameSpace, "select Buyer_PKId from Sales_Buyer_Accounting_Template where Buyer_ID=" & iBM_Id & " And Buyer_Desc='TIN' and Buyer_CompId=" & iCompID & " ")
            If iBuyerExistingID > 0 Then
                ssql = "" : ssql = "Update Sales_Buyer_Accounting_Template Set Buyer_Value='" & TINNo & "' Where Buyer_pkID=" & iBuyerExistingID & " And Buyer_ID=" & iBM_Id & " And Buyer_Desc='TIN' And Buyer_CompID=" & iCompID & " "
                objDBL.SQLExecuteNonQuery(sNameSpace, ssql)
            Else
                iBuyerPKID = objDBL.SQLExecuteScalar(sNameSpace, "Select  IsNull(MAX(Buyer_pkID),0)+1 from Sales_Buyer_Accounting_Template")
                ssql = "" : ssql = "Insert Into Sales_Buyer_Accounting_Template (Buyer_pkID,Buyer_ID,Buyer_Desc,Buyer_Value,Buyer_Status,Buyer_CompID)"
                ssql = ssql & "Values(" & iBuyerPKID & "," & iBM_Id & ",'TIN','" & TINNo & "','W'," & iCompID & ")"
                objDBL.SQLExecuteNonQuery(sNameSpace, ssql)
            End If

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function CheckCityExistOrNot(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sCity As String, ByVal iMaster As Integer) As Integer
        Dim sSql As String
        Dim dr As OleDb.OleDbDataReader
        Try
            If iMaster = 0 Then
                sSql = "" : sSql = "Select * from acc_General_master where Mas_Desc ='" & sCity & "' and Mas_Master = 3"
            Else
                sSql = "" : sSql = "Select * from acc_General_master where Mas_Desc ='" & sCity & "' and Mas_Master = 4"
            End If

            dr = objDBL.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows = True Then
                dr.Read()
                CheckCityExistOrNot = dr("Mas_Id")
            Else
                CheckCityExistOrNot = 0
            End If
            dr.Close()
            Return CheckCityExistOrNot
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Shared Function RemoveQuote(ByVal sString As String) As String
        Try
            RemoveQuote = Trim(Replace(sString, "'", "`"))
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
