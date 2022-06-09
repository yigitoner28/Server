Imports System
Imports System.Net
Imports System.Net.Sockets
Imports System.Text


Module MainModule

    Dim _server As TcpListener


    Sub Main(args As String())

        Try


            Dim ip As String = "192.168.1.38"
            Dim port As Integer = 8080

            _server = New TcpListener(IPAddress.Parse(ip), port)
            _server.Start()

            Threading.ThreadPool.QueueUserWorkItem(AddressOf NewClient)



        Catch ex As Exception
            Console.WriteLine(ex)
        End Try
        Console.ReadLine()
    End Sub

    Private Sub NewClient(state As Object)
        Try

            Using client As TcpClient = _server.AcceptTcpClient()

                Threading.ThreadPool.QueueUserWorkItem(AddressOf NewClient)



                Using ns As NetworkStream = client.GetStream()



                    While True

                        Dim toReceive(100000) As Byte

                        Dim lenght As Integer = ns.Read(toReceive, 0, toReceive.Length)

                        Dim text As String = Encoding.ASCII.GetString(toReceive, 0, lenght)

                        Console.WriteLine(text)
                        Console.WriteLine()





                        Dim toSend() As Byte = Encoding.ASCII.GetBytes("CLient  :  " + text)





                        Console.WriteLine()
                        ns.Write(toSend, 0, toSend.Length)









                    End While
                End Using

            End Using


        Catch ex As Exception
            Console.WriteLine(ex)
        End Try
    End Sub
End Module
