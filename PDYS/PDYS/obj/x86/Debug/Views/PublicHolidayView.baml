   M S B A M L   `   `   `  ����K  GPDYS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b541947c23086f5b5clr-namespace:PDYS.UserControlsPDYS.UserControls  5      "    PDYS.Views.PublicHolidayView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      b ^DevExpress.Xpf.Core.v11.2, Version=11.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      (ucclr-namespace:PDYS.UserControls  5      $	��400��5      $	��250��5	      .��5      �   � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��10 �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��5      $��Center=�5      $��
Tatil Adı��5       %  DevExpress.Xpf.Editors.TextEdit  5#   
   $��0��6   $��2��5$      $��3q�5%      %  DevExpress.Xpf.Editors.BaseEdit   	EditValue 5&      �� *Name+ �� Mode$ TwoWay=� �� ValidatesOnDataErrors . �� 5*   
   $��1��6   $��0��5+      $��Center=�5,      $��Başlangıç Tarihi��5-      %  DevExpress.Xpf.Editors.DateEdit  5/   
   $��1��6   $��2��50      $	��150��51      $��3q�52      $
��Left=�53      $��Center=�54         AllowNullInput .  56      �� *	StartDate+$ TwoWay=� . �� 5:   
   $��2��6   $��0��5;      $��Center=�5<      $��Bitiş Tarihi��5=        5@   
   $��2��6   $��2��5A      $	��150��5B      $��3q�5C      $
��Left=�5D      $��Center=�5E       .  5G      �� *	EndDate+$ TwoWay=� . �� 5K   
   $��3��6   $��0��5L      $��Center=�5M      $��Mesai Katsayısı��5N        5P   
   $��3��6   $��2��5Q      $��80��5R      $��3q�5S      $
��Left=�5T      $��Center=�5U         Mask$ p0��5Y         MaskAllowNullInput .     MaskIgnoreBlank .  	   MaskType$	 Numeric=�5\      
   MaskUseAsDisplayFormat
 .  5V      �� *OvertimeFactor+$ TwoWay=� . 5]      �� 5_   
   $��4��6   $��0��5`      $��3��5a      $��0,10,0,0q�5b      $��Stretch=�5c      $��
Açıklama��5d      .��5f        $��6��6   $��2��5g      $��3q�5h      $��Stretch=�5i      $��Stretch=�5j      $	��Top=�5k      )  #DevExpress.Xpf.Editors.TextEditBase   AcceptsReturn .    TextWrapping$
 Wrap=�5p      "   VerticalScrollBarVisibility$ Visible=�5q         Text 5m      �� *Description+$ TwoWay=� . 5q      5r      5t      5w      