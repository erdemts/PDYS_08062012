   M S B A M L   `   `   `  ����K  GPDYS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b541947c23086f5b)clr-namespace:PDYS.HelperPDYS.Helper  5      9!clr-namespace:PDYS.InfraStructurePDYS.InfraStructure  5	      5clr-namespace:PDYS.UserControlsPDYS.UserControls  5      (    "PDYS.Views.EmployeeInputOutputView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      b ^DevExpress.Xpf.Core.v11.2, Version=11.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5      b ^DevExpress.Xpf.Grid.v11.2, Version=11.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49ag cDevExpress.Xpf.Grid.v11.2.Core, Version=11.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid   5      (helperclr-namespace:PDYS.Helper   5      c	 _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Cia:http://schemas.microsoft.com/expression/2010/interactivity 	 5      -infra!clr-namespace:PDYS.InfraStructure 	 5	      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	 5
      (ucclr-namespace:PDYS.UserControls 	 5      $	��400��5      $	��300��5      .��5      � $��1��6   {�6   �� *
IsEditable+  �� Mode$  TwoWay=� � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $	��100 �6   �� 5      $��10 �6   �� 5      $��* �6   .+�5!   
   �� $��0��6   $��0��5"      $��Center=�5#      $��Personel��5$      $   PDYS.UserControls.ExtendLookup  5&   
   $��0��6   $��2��5'      $��3q�5(         LookupViewModel 5)      �� *LookupEmployee+   SelectedDataModel 5*      �� *
Employee+$  TwoWay=� �� ValidatesOnDataErrors . �� 52   
   $��2��6   $��0��53      $��Center=�54      $��Okuyucu Cihaz��55        57   
   $��2��6   $��2��58      $��3q�59       5:      �� *LookupReaderDevice+ 5;      �� *ReaderDevice+$  TwoWay=� . �� 5@   
   $��3��6   $��0��5A      $��Center=�5B      $��Giriş/Çıkış Tarihi��5C      %  DevExpress.Xpf.Editors.DateEdit  5F   
   $��3��6   $��2��5G      $	��150��5H      $��3q�5I      $
��Left=�5J      $��Center=�5K      %  DevExpress.Xpf.Editors.TextEdit   Mask$ dd.MM.yyyy HH:mm:ss��5O         MaskType$ DateTime=�5P         MaskUseAsDisplayFormat . %  DevExpress.Xpf.Editors.BaseEdit	   	EditValue	 5L      �� *InputOutputDate+$  TwoWay=� . 5Q      �� 5T   
   $��4��6   $��0��5U      $��Center=�5V      $��Hareket Tipi��5W      �� 5Y   
   $��4��6   $��2��5Z      
 �� 
IsEditable
 .  ��5\      �� *ListInputOutputType+ �� SelectedItem 5]      �� *SelectedInputOutputType+$  TwoWay=� . 5b      5c      