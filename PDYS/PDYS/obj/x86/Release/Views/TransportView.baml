   M S B A M L   `   `   `  ���� K  GPDYS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b541947c23086f5b9!clr-namespace:PDYS.InfraStructurePDYS.InfraStructure  5clr-namespace:PDYS.UserControlsPDYS.UserControls      PDYS.Views.TransportView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     a ]DevExpress.Xpf.Core.v11.1, Version=11.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Cia:http://schemas.microsoft.com/expression/2010/interactivity  -infra!clr-namespace:PDYS.InfraStructure  Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  (ucclr-namespace:PDYS.UserControls  $	��750��$	��400��.���   � ColumnDefinitions  �� $��4* ��� $��6* �.+�� $��0�� � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��* �  �� $
��Auto ��� $��10 ��� $��* �.+��� $��0��$��0��$��Center=�$��Servis Adı��%  DevExpress.Xpf.Editors.TextEdit  $��0��$��2��$��3q�$��Center=�%  DevExpress.Xpf.Editors.BaseEdit   	EditValue �� *Name+ �� Mode$ TwoWay=� �� ValidatesOnDataErrors . �� $��1��$��0��$��Center=�$��Araç Bilgisi��  $��1��$��2��$��3q�$��Center=� �� *VhicleInformation+$ TwoWay=� . �� $��2��$��0��$��Center=�$��Plaka��  $��2��$��2��$	��150��$��3q�$
��Left=�$��Center=� �� *NumberPlate+$ TwoWay=� . �� $��3��$��0��$��Center=�$��Sürücü Adı��  $��3��$��2��$��3q�$��Center=� �� *
DriverName+$ TwoWay=� . �� $��4��$��0��$��Center=�$��Sürücü Telefon��  $��4��$��2��$��3q�$��Center=� �� *DriverPhone+$ TwoWay=� . �� $��5��$��0��$��3��$��0,10,0,0q�$��Stretch=�$��
Açıklama��.��  $��3q�$��Stretch=�$	��Top=�)  #DevExpress.Xpf.Editors.TextEditBase   AcceptsReturn .    TextWrapping$
 Wrap=� �� *Description+$ TwoWay=� . � $��1��$��5,0,0,0q�{��� *IsNavigationEnabled+ �� $
��Auto ��� $��* � � 	Resources &   System.Windows.Controls.DataGrid   EntityDataGrid   AutoGenerateColumns	 �� SelectedItem.  (System.Windows.Interactivity.Interaction
   Triggers/  )System.Windows.Interactivity.EventTrigger   	EventName.  (System.Windows.Interactivity.TriggerBase   Actions.   (PDYS.InfraStructure.ExecuteCommandAction   Command   Columns0	  *System.Windows.Controls.DataGridTextColumn,
  &System.Windows.Controls.DataGridColumn 
  Width5  /System.Windows.Controls.DataGridLengthConverter 
  Header 
  
IsReadOnly1  +System.Windows.Controls.DataGridBoundColumn   Binding��%�   &	          .  ���� *Items+	 �� *SelectedItem+$ TwoWay=�
   $ MouseDoubleClick��.    �� *MouseDoubleClickCommand+ 	  $	 300 $
 Adı�� .  �� *DisplayName+.+��� $��0��$��Stretch=�$��Center=�$��14(�$
��Bold"�  HeaderFontColor#��[  �� Padding$ 5,3,10,3q�$��Personel Listesi������   
WhiteColor �� Color# �  "   PDYS.UserControls.EntityList  $��1��$��0,5,0,0q�   GridContent# [   � DataContext �� *TransportEmployees+