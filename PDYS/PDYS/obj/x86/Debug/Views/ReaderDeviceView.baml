   M S B A M L   `   `   `  ����K  GPDYS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b541947c23086f5b)clr-namespace:PDYS.HelperPDYS.Helper  5      9!clr-namespace:PDYS.InfraStructurePDYS.InfraStructure  5	      5clr-namespace:PDYS.UserControlsPDYS.UserControls  5      !    PDYS.Views.ReaderDeviceView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      b ^DevExpress.Xpf.Core.v11.2, Version=11.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5      b ^DevExpress.Xpf.Grid.v11.2, Version=11.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49ag cDevExpress.Xpf.Grid.v11.2.Core, Version=11.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid   5      (helperclr-namespace:PDYS.Helper   5      c	 _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Cia:http://schemas.microsoft.com/expression/2010/interactivity 	 5      -infra!clr-namespace:PDYS.InfraStructure 	 5	      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	 5
      (ucclr-namespace:PDYS.UserControls 	 5      $	��400��5      $	��300��5      .��5      � $��1��6     � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $	��120 �6   �� 5       $��10 �6   �� 5!      $��* �6   .+�5%   
   �� $��0��6   $��0��5&      $��Center=�5'      $��
Cihaz Adı��5(      %  DevExpress.Xpf.Editors.TextEdit  5*   
   $��0��6   $��2��5+      $��3q�5,      $��Center=�5-      %  DevExpress.Xpf.Editors.BaseEdit   	EditValue 5.      �� *Name+ �� Mode$ TwoWay=� �� ValidatesOnDataErrors . �� 52   
   $��1��6   $��0��53      $��Center=�54      $��Hareket Tipi��55      �� 57   
   $��1��6   $��2��58       �� 
IsEditable .  ��5:      �� *ListInputOutputType+ �� SelectedItem 5;      �� *SelectedInputOutputType+$ TwoWay=� . �� 5?   
   $��2��6   $��0��5@      $��Center=�5A      $��	IP Adresi��5B        5D   
   $��2��6   $��2��5E      $	��150��5F      $��3q�5G      $
��Left=�5H      $��Center=�5I         Mask$G A(([01]?\d\d?|2[0-4]\d|25[0-5])\.){3}([01]?\d\d?|25[0-5]|2[0-4]\d)��5M         MaskIgnoreBlank .  	   MaskType$	 RegEx=�5O      
   MaskUseAsDisplayFormat
 .  5J      �� *IP+$ TwoWay=� . 5P      �� 5R   
   $��3��6   $��0��5S      $��Center=�5T      $��Port Numrası��5U        5W   
   $��3��6   $��2��5X      $	��100��5Y      $��3q�5Z      $
��Left=�5[      $��Center=�5\      $ \d+(\.\d(\d)?)?��5`       .  $	 RegEx=�5b      
 .  5]      �� *Port+$ TwoWay=� . 5c      �� 5e   
   $��5��6   $��0��5f      $��Center=�5g      $��Bağlantı Şifresi��5h        5j   
   $��5��6   $��2��5k      $	��100��5l      $��3q�5m      $
��Left=�5n      $��Center=�5o      $ \d*.\d?��5s         MaskAllowNullInput .     MaskAutoComplete$
 None=�5u       .     MaskShowPlaceHolders .  $	 RegEx=�5x      
 . )  #DevExpress.Xpf.Editors.TextEditBase   	MaxLength$ 6��5z       5p      �� *ComKey+$ TwoWay=� . 5z      �� 5}   
   $��6��6   $��0��5~      $��Center=�5      $��Cihaz Seri Numarası��5�      ]� 5�   
   $��6��6   $��2��5�      $��3q�5�      .+�5�      �� $��5,0,0,0q�6   $��Right=�5�      $��Center=�5�      $��Cihazdan Oku��5�      $��Right=�5�      $��5,0,5,0q�5�      ��5�      �� *ReadDeviceSerialCommand+5�        5�      $��Center=�6    66   �� *SerialNumber+$ TwoWay=� . 5�      �� 5�   
   $��7��6   $��3��5�      $��3q�5�      .+�5�      �� $��0,5,0,0q�6   $��Center=�5�      $��Center=�5�      $ ��Cihaz Tarih/Saat Güncelle��5�      $��	10,0,10,0q�5�      ��5�      �� *SetDeviceTimeCommand+5�      5�      �� 5�   
   $��8��6   $��0��5�      $��3��5�      $��0,10,0,0q�5�      $��Stretch=�5�      $��
Açıklama��5�      .��5�        $��3q�6   $��Stretch=�5�      $	��Top=�5�         AcceptsReturn .    TextWrapping$
 Wrap=�5�       5�      �� *Description+$ TwoWay=� . 5�      5�      5�      5�      