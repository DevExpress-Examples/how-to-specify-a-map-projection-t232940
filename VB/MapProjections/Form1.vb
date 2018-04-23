﻿Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms
Imports DevExpress.XtraMap

Namespace MapProjections
    Partial Public Class Form1
        Inherits Form

        Private Const filepath As String = "..\..\Data\Countries.shp"
        Private mapProjections As New List(Of ProjectionBase)() From { _
            New BraunStereographicProjection(), _
            New EllipticalMercatorProjection(), _
            New EqualAreaProjection(), _
            New EquidistantProjection(), _
            New EquirectangularProjection(), _
            New KavrayskiyProjection(), _
            New LambertCylindricalEqualAreaProjection(), _
            New MillerProjection(), _
            New SinusoidalProjection(), _
            New SphericalMercatorProjection() _
        }

        Private ReadOnly Property CoordinateSystem() As GeoMapCoordinateSystem
            Get
                Return TryCast(mapControl.CoordinateSystem, GeoMapCoordinateSystem)
            End Get
        End Property

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            lbProjection.DataSource = mapProjections
            lbProjection.SetSelected(0, True)

            Dim baseUri As New Uri(System.Reflection.Assembly.GetEntryAssembly().Location)
            Dim uri As New Uri(baseUri, filepath)
            mapControl.Layers.Add(New VectorItemsLayer() With { _
                .Data = New ShapefileDataAdapter() With {.FileUri = uri} _
            })
        End Sub

        Private Sub lbProjection_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lbProjection.SelectedIndexChanged
            CoordinateSystem.Projection = TryCast(lbProjection.SelectedValue, ProjectionBase)
        End Sub
    End Class
End Namespace
