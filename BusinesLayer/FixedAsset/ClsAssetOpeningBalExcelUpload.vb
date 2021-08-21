Imports System
Imports System.Data
Imports DatabaseLayer
Imports BusinesLayer
Public Class ClsAssetOpeningBalExcelUpload
    Private objDBL As New DatabaseLayer.DBHelper

    Private AFAA_ID As Integer
    Private AFAA_AssetTrType As Integer
    Private AFAA_CurrencyType As Integer
    Private AFAA_CurrencyAmnt As Double
    Private AFAA_Zone As Integer
    Private AFAA_Region As Integer
    Private AFAA_Area As Integer
    Private AFAA_Branch As Integer
    Private AFAA_ActualLocn As String
    Private AFAA_SupplierName As Integer
    Private AFAA_SupplierCode As Integer
    Private AFAA_TrType As Integer
    Private AFAA_AssetType As String
    Private AFAA_AssetNo As String
    Private AFAA_AssetRefNo As String
    Private AFAA_Description As String
    Private AFAA_ItemCode As String
    Private AFAA_ItemDescription As String
    Private AFAA_Quantity As Integer
    Private AFAA_CommissionDate As Date
    Private AFAA_PurchaseDate As Date
    Private AFAA_AssetAge As Double
    Private AFAA_AssetAmount As Double
    Private AFAA_AssetDelID As Integer
    Private AFAA_AssetDelDate As Date
    Private AFAA_AssetDeletionDate As Date
    Private AFAA_Assetvalue As Double
    Private AFAA_AssetDesc As String
    Private AFAA_CreatedBy As Integer
    Private AFAA_CreatedOn As Date
    Private AFAA_UpdatedBy As Integer
    Private AFAA_UpdatedOn As Date
    Private AFAA_ApprovedBy As Integer
    Private AFAA_ApprovedOn As Date
    Private AFAA_Deletedby As Integer
    Private AFAA_DeletedOn As Date
    Private AFAA_Status As String
    Private AFAA_Delflag As String
    Private AFAA_YearID As Integer
    Private AFAA_CompID As Integer
    Private AFAA_Operation As String
    Private AFAA_IPAddress As String
    Private AFAA_AddnType As String
    Private AFAA_DelnType As String
    Private AFAA_Depreciation As Double

    Public Property dAFAA_Depreciation() As Double
        Get
            Return (AFAA_Depreciation)
        End Get
        Set(ByVal Value As Double)
            AFAA_Depreciation = Value
        End Set
    End Property
    Public Property sAFAA_DelnType() As String
        Get
            Return (AFAA_DelnType)
        End Get
        Set(ByVal Value As String)
            AFAA_DelnType = Value
        End Set
    End Property
    Public Property sAFAA_AddnType() As String
        Get
            Return (AFAA_AddnType)
        End Get
        Set(ByVal Value As String)
            AFAA_AddnType = Value
        End Set
    End Property
    Public Property iAFAA_ID() As Integer
        Get
            Return AFAA_ID
        End Get
        Set(ByVal value As Integer)
            AFAA_ID = value
        End Set
    End Property
    Public Property iAFAA_AssetTrType() As Integer
        Get
            Return AFAA_AssetTrType
        End Get
        Set(value As Integer)
            AFAA_AssetTrType = value
        End Set
    End Property
    Public Property dAFAA_CurrencyAmnt() As Double
        Get
            Return AFAA_CurrencyAmnt
        End Get
        Set(value As Double)
            AFAA_CurrencyAmnt = value
        End Set
    End Property
    Public Property iAFAA_Zone() As Integer
        Get
            Return AFAA_Zone
        End Get
        Set(value As Integer)
            AFAA_Zone = value
        End Set
    End Property
    Public Property iAFAA_Region() As Integer
        Get
            Return AFAA_Region
        End Get
        Set(value As Integer)
            AFAA_Region = value
        End Set
    End Property
    Public Property iAFAA_Area() As Integer
        Get
            Return AFAA_Area
        End Get
        Set(value As Integer)
            AFAA_Area = value
        End Set
    End Property
    Public Property iAFAA_Branch() As Integer
        Get
            Return AFAA_Branch
        End Get
        Set(value As Integer)
            AFAA_Branch = value
        End Set
    End Property
    Public Property sAFAA_ActualLocn() As String
        Get
            Return AFAA_ActualLocn
        End Get
        Set(value As String)
            AFAA_ActualLocn = value
        End Set
    End Property
    Public Property iAFAA_SupplierName() As Integer
        Get
            Return AFAA_SupplierName
        End Get
        Set(value As Integer)
            AFAA_SupplierName = value
        End Set
    End Property
    Public Property iAFAA_SupplierCode() As Integer
        Get
            Return AFAA_SupplierCode
        End Get
        Set(value As Integer)
            AFAA_SupplierCode = value
        End Set
    End Property
    Public Property iAFAA_CurrencyType() As Integer
        Get
            Return AFAA_CurrencyType
        End Get
        Set(value As Integer)
            AFAA_CurrencyType = value
        End Set
    End Property
    Public Property iAFAA_TrType() As Integer
        Get
            Return AFAA_TrType
        End Get
        Set(ByVal value As Integer)
            AFAA_TrType = value
        End Set
    End Property
    Public Property sAFAA_AssetType() As String
        Get
            Return AFAA_AssetType
        End Get
        Set(ByVal value As String)
            AFAA_AssetType = value
        End Set
    End Property
    Public Property sAFAA_AssetNo() As String
        Get
            Return AFAA_AssetNo
        End Get
        Set(ByVal value As String)
            AFAA_AssetNo = value
        End Set
    End Property
    Public Property sAFAA_AssetRefNo() As String
        Get
            Return AFAA_AssetRefNo
        End Get
        Set(ByVal value As String)
            AFAA_AssetRefNo = value
        End Set
    End Property
    Public Property sAFAA_Description() As String
        Get
            Return AFAA_Description
        End Get
        Set(ByVal value As String)
            AFAA_Description = value
        End Set
    End Property
    Public Property sAFAA_ItemCode() As String
        Get
            Return AFAA_ItemCode
        End Get
        Set(ByVal value As String)
            AFAA_ItemCode = value
        End Set
    End Property
    Public Property sAFAA_ItemDescription() As String
        Get
            Return AFAA_ItemDescription
        End Get
        Set(ByVal value As String)
            AFAA_ItemDescription = value
        End Set
    End Property
    Public Property iAFAA_Quantity() As Integer
        Get
            Return AFAA_Quantity
        End Get
        Set(ByVal value As Integer)
            AFAA_Quantity = value
        End Set
    End Property
    Public Property dAFAA_CommissionDate() As Date
        Get
            Return AFAA_CommissionDate
        End Get
        Set(ByVal value As Date)
            AFAA_CommissionDate = value
        End Set
    End Property
    Public Property dAFAA_PurchaseDate() As Date
        Get
            Return AFAA_PurchaseDate
        End Get
        Set(ByVal value As Date)
            AFAA_PurchaseDate = value
        End Set
    End Property
    Public Property dAFAA_AssetAge() As Double
        Get
            Return AFAA_AssetAge
        End Get
        Set(ByVal value As Double)
            AFAA_AssetAge = value
        End Set
    End Property
    Public Property dAFAA_AssetAmount() As Double
        Get
            Return AFAA_AssetAmount
        End Get
        Set(ByVal value As Double)
            AFAA_AssetAmount = value
        End Set
    End Property
    Public Property iAFAA_AssetDelID() As Integer
        Get
            Return AFAA_AssetDelID
        End Get
        Set(ByVal value As Integer)
            AFAA_AssetDelID = value
        End Set
    End Property
    Public Property dAFAA_AssetDelDate() As Date
        Get
            Return AFAA_AssetDelDate
        End Get
        Set(ByVal value As Date)
            AFAA_AssetDelDate = value
        End Set
    End Property
    Public Property dAFAA_AssetDeletionDate() As Date
        Get
            Return AFAA_AssetDeletionDate
        End Get
        Set(ByVal value As Date)
            AFAA_AssetDeletionDate = value
        End Set
    End Property

    Public Property dAFAA_Assetvalue() As Double
        Get
            Return AFAA_Assetvalue
        End Get
        Set(ByVal value As Double)
            AFAA_Assetvalue = value
        End Set
    End Property

    Public Property sAFAA_AssetDesc() As String
        Get
            Return AFAA_AssetDesc
        End Get
        Set(ByVal value As String)
            AFAA_AssetDesc = value
        End Set
    End Property
    Public Property iAFAA_CreatedBy() As Integer
        Get
            Return AFAA_CreatedBy
        End Get
        Set(ByVal value As Integer)
            AFAA_CreatedBy = value
        End Set
    End Property
    Public Property dAFAA_CreatedOn() As Date
        Get
            Return AFAA_CreatedOn
        End Get
        Set(ByVal value As Date)
            AFAA_CreatedOn = value
        End Set
    End Property
    Public Property iAFAA_UpdatedBy() As Integer
        Get
            Return AFAA_UpdatedBy
        End Get
        Set(ByVal value As Integer)
            AFAA_UpdatedBy = value
        End Set
    End Property
    Public Property dAFAA_UpdatedOn() As Date
        Get
            Return AFAA_UpdatedOn
        End Get
        Set(ByVal value As Date)
            AFAA_UpdatedOn = value
        End Set
    End Property
    Public Property iAFAA_ApprovedBy() As Integer
        Get
            Return AFAA_ApprovedBy
        End Get
        Set(ByVal value As Integer)
            AFAA_ApprovedBy = value
        End Set
    End Property
    Public Property dAFAA_ApprovedOn() As Date
        Get
            Return AFAA_ApprovedOn
        End Get
        Set(ByVal value As Date)
            AFAA_ApprovedOn = value
        End Set
    End Property
    Public Property dAFAA_Deletedby() As Integer
        Get
            Return AFAA_Deletedby
        End Get
        Set(ByVal value As Integer)
            AFAA_Deletedby = value
        End Set
    End Property
    Public Property dAFAA_DeletedOn() As Date
        Get
            Return AFAA_DeletedOn
        End Get
        Set(ByVal value As Date)
            AFAA_DeletedOn = value
        End Set
    End Property
    Public Property sAFAA_Status() As String
        Get
            Return AFAA_Status
        End Get
        Set(ByVal value As String)
            AFAA_Status = value
        End Set
    End Property
    Public Property sAFAA_Delflag() As String
        Get
            Return AFAA_Delflag
        End Get
        Set(ByVal value As String)
            AFAA_Delflag = value
        End Set
    End Property
    Public Property iAFAA_YearID() As Integer
        Get
            Return AFAA_YearID
        End Get
        Set(ByVal value As Integer)
            AFAA_YearID = value
        End Set
    End Property
    Public Property iAFAA_CompID() As Integer
        Get
            Return AFAA_CompID
        End Get
        Set(ByVal value As Integer)
            AFAA_CompID = value
        End Set
    End Property
    Public Property sAFAA_Operation() As String
        Get
            Return AFAA_Operation
        End Get
        Set(ByVal value As String)
            AFAA_Operation = value
        End Set
    End Property
    Public Property sAFAA_IPAddress() As String
        Get
            Return AFAA_IPAddress
        End Get
        Set(ByVal value As String)
            AFAA_IPAddress = value
        End Set
    End Property
    Public Function LoadAccZone(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Org_Node,Org_Name From Sad_Org_Structure Where Org_Parent in(Select Org_Node From Sad_Org_Structure Where Org_Parent=0 and Org_CompID=" & iCompID & " )"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAccArea(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAccRgn As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Org_Node,Org_Name From Sad_Org_Structure Where Org_Parent=" & iAccRgn & " And Org_CompID=" & iCompID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAccRgn(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAccZone As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Org_Node,Org_Name From Sad_Org_Structure Where Org_Parent=" & iAccZone & " And Org_CompID=" & iCompID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAccBrnch(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAccarea As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Org_Node,Org_Name From Sad_Org_Structure Where Org_Parent=" & iAccarea & " And Org_CompID=" & iCompID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadCurrencyID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sCurrencyname As String) As Integer
        Dim sSql As String = ""
        Dim iId As Integer
        Try
            sSql = "Select CUR_ID from Currency_Master where  CUR_CountryName ='" & sCurrencyname & "' and CUR_Status='A'"
            iId = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            If iId > 0 Then
                Return iId
            Else
                iId = 0
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCurrencyName(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sCurrencyname As String) As String
        Dim sSql As String = ""
        Dim sCode As String
        Try
            sSql = "Select CUR_CODE from Currency_Master where  CUR_CountryName ='" & sCurrencyname & "' and CUR_Status='A'"
            sCode = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            If sCode <> "" Then
                Return sCode
            Else
                sCode = ""
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSupplierID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sSupplierName As String) As Integer
        Dim sSql As String = ""
        Dim iId As Integer
        Try
            sSql = "Select CSM_ID From customerSupplierMaster Where CSM_Name='" & sSupplierName & "'  and  CSM_CompID=" & iCompID & ""
            iId = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            If iId > 0 Then
                Return iId
            Else
                iId = 0
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSupplierID1(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sSupplierName As String, ByVal sSuplierCode As String) As Integer
        Dim sSql As String = ""
        ' Dim bCheck As Boolean
        Dim iSupId As Integer
        Try
            sSql = "Select CSM_ID From customerSupplierMaster Where CSM_Name='" & sSupplierName & "' and CSM_Code='" & sSuplierCode & "' and  CSM_CompID=" & iCompID & ""
            iSupId = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            If iSupId > 0 Then
                Return iSupId
            Else
                iSupId = 0
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSupplierName(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sSupplierName As String) As Integer
        Dim sSql As String = ""
        Dim iID As Integer
        Try
            sSql = "Select CSM_ID From customerSupplierMaster Where CSM_Name='" & sSupplierName & "' and CSM_CompID=" & iCompID & ""
            iID = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            If iID > 0 Then
                Return iID
            Else
                iID = 0
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAssetType(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sSupplierName As String) As Integer
        Dim sSql As String = ""
        Dim iAssetType As Integer
        Try
            sSql = "Select gl_ID From Chart_Of_Accounts where gl_desc='" & sSupplierName & "' and gl_CompId=" & iCompID & ""
            iAssetType = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            If iAssetType > 0 Then
                Return iAssetType
            Else
                iAssetType = 0
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckExistorNot(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAssetType As Integer, ByVal sRefNo As String)
        Dim sSql As String = ""
        Dim dBcheck As Boolean
        Try
            sSql = "Select AFAA_ID From Acc_FixedAssetAdditionDel where AFAA_AssetType=" & iAssetType & " and AFAA_AssetRefNo='" & sRefNo & "' and AFAA_CompID=" & iCompID & ""
            dBcheck = objDBL.SQLCheckForRecord(sNameSpace, sSql)
            If dBcheck = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAssetType1(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sSupplierName As String)
        Dim sSql As String = ""
        Dim dBcheck As Boolean
        Try
            sSql = "Select gl_desc From Chart_Of_Accounts where gl_desc='" & sSupplierName & "' and gl_CompId=" & iCompID & ""
            dBcheck = objDBL.SQLCheckForRecord(sNameSpace, sSql)
            If dBcheck = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetGLID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sglDesc As String) As Integer
        Dim sSql As String = ""
        Dim Grp As Integer
        Dim iglID As Integer
        Try
            sSql = "select gl_ID from Chart_Of_Accounts where gl_desc='" & sglDesc & "' and gl_CompId=" & iCompID & ""
            iglID = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            sSql = "select IsNull(count(*),0)+1 from acc_fixedAssetAdditionDel where AFAA_AssetType='" & iglID & "' and AFAA_CompID=" & iCompID & ""
            Grp = Convert.ToString(objDBL.SQLExecuteScalar(sNameSpace, sSql))
            Return Grp
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAssetTypeNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sglDesc As String) As String
        Dim sSql As String = ""
        Dim iglID As Integer
        Try
            sSql = "select gl_ID from Chart_Of_Accounts where gl_desc='" & sglDesc & "' and gl_CompId=" & iCompID & ""
            iglID = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            sSql = "select gl_glcode from chart_of_accounts where gl_id=" & iglID & " and gl_CompId=" & iCompID & ""
            GetAssetTypeNo = objDBL.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAssetNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sglDesc As String) As String
        Dim sSql As String = ""
        Dim iglID As Integer
        Try
            sSql = "select gl_ID from Chart_Of_Accounts where gl_desc='" & sglDesc & "' and gl_CompId=" & iCompID & ""
            iglID = objDBL.SQLExecuteScalar(sNameSpace, sSql)
            sSql = "select gl_glcode from Chart_Of_Accounts where gl_id=" & iglID & " and gl_CompId=" & iCompID & ""
            LoadAssetNo = objDBL.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveFixedAssetAddition(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objOPExcel As ClsAssetOpeningBalExcelUpload) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(42) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_AssetTrType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_AssetTrType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_CurrencyType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_CurrencyType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_CurrencyAmnt", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_CurrencyAmnt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_Zone", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_Zone
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_Region", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_Region
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_Area", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_Area
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_Branch", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_Branch
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_ActualLocn", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_ActualLocn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_SupplierName", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_SupplierName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_SupplierCode", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_SupplierCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_TrType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_TrType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_AssetType", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_AssetType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_AssetNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_AssetNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_AssetRefNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_AssetRefNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_Description", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_Description
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_ItemCode", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_ItemCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_ItemDescription", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_ItemDescription
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_Quantity", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_Quantity
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_CommissionDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_CommissionDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_PurchaseDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_PurchaseDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_AssetAge", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_AssetAge
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_AssetAmount", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_AssetAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_AssetDelID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_AssetDelID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_AssetDelDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_AssetDelDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_AssetDeletionDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_AssetDeletionDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_Assetvalue", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_Assetvalue
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_AssetDesc", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_AssetDesc
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_UpdatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_UpdatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_Status", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_Delflag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_Delflag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_Operation", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_Operation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_IPAddress", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_AddnType", OleDb.OleDbType.VarChar, 5)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_AddnType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_DelnType", OleDb.OleDbType.VarChar, 5)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_DelnType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAA_Depreciation", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objOPExcel.AFAA_Depreciation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spAcc_FixedAssetAdditionDel", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
