Public Class USB
    '
    'The messages to look for.
    Private Const WM_DEVICECHANGE As Integer = &H219
    Private Const DBT_DEVICEARRIVAL As Integer = &H8000
    Private Const DBT_DEVICEREMOVECOMPLETE As Integer = &H8004
    Private Const DBT_DEVTYP_VOLUME As Integer = &H2  '
    '
    'Get the information about the detected volume.
    Private Structure DEV_BROADCAST_VOLUME
        Dim Dbcv_Size As Integer
        Dim Dbcv_Devicetype As Integer
        Dim Dbcv_Reserved As Integer
        Dim Dbcv_Unitmask As Integer
        Dim Dbcv_Flags As Short

    End Structure
    Protected Overrides Sub WndProc(ByRef M As Windows.Forms.Message)
        '
        'These are the required subclassing codes for detecting device based removal and arrival.
        '
        If M.Msg = WM_DEVICECHANGE Then
            Select Case M.WParam
                '
                'Check if a device was added.
                Case DBT_DEVICEARRIVAL
                    Dim DevType As Integer = Runtime.InteropServices.Marshal.ReadInt32(M.LParam, 4)
                    If DevType = DBT_DEVTYP_VOLUME Then
                        Dim Vol As New DEV_BROADCAST_VOLUME
                        Vol = Runtime.InteropServices.Marshal.PtrToStructure(M.LParam, GetType(DEV_BROADCAST_VOLUME))
                        If Vol.Dbcv_Flags = 0 Then
                            For i As Integer = 0 To 20
                                If Math.Pow(2, i) = Vol.Dbcv_Unitmask Then
                                    Dim Usb As String = Chr(65 + i) + ":\"
                                    MsgBox("Looks like a USB device was plugged in!" & vbNewLine & vbNewLine & "The drive letter is: " & Usb.ToString)
                                    Exit For
                                End If
                            Next
                        End If
                    End If
                '
                'Check if the message was for the removal of a device.
                Case DBT_DEVICEREMOVECOMPLETE
                    Dim DevType As Integer = Runtime.InteropServices.Marshal.ReadInt32(M.LParam, 4)
                    If DevType = DBT_DEVTYP_VOLUME Then
                        Dim Vol As New DEV_BROADCAST_VOLUME
                        Vol = Runtime.InteropServices.Marshal.PtrToStructure(M.LParam, GetType(DEV_BROADCAST_VOLUME))
                        If Vol.Dbcv_Flags = 0 Then
                            For i As Integer = 0 To 20
                                If Math.Pow(2, i) = Vol.Dbcv_Unitmask Then
                                    Dim Usb As String = Chr(65 + i) + ":\"
                                    MsgBox("Looks like a volume device was removed!" & vbNewLine & vbNewLine & "The drive letter is: " & Usb.ToString)
                                    Exit For
                                End If
                            Next
                        End If
                    End If
            End Select
        End If
        MyBase.WndProc(M)
    End Sub
End Class