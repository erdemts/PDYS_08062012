   M S B A M L   `   `   `  ����K  GPDYS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b541947c23086f5b5clr-namespace:PDYS.UserControlsPDYS.UserControls  5      4    .PDYS.Report.Views.RptEmployeeSalaryScoringView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      b ^DevExpress.Xpf.Core.v11.2, Version=11.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  5      ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5      f bDevExpress.Xpf.Printing.v11.2, Version=11.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aBdxp6http://schemas.devexpress.com/winfx/2008/xaml/printing   5      *ucclr-namespace:PDYS.UserControls   5      &   DevExpress.Xpf.Core.ThemeManager    	ThemeName0  *DevExpress.Xpf.Core.ThemeNameTypeConverter$  DeepBlue 5      .��5	      �  � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0,0,0,5q�6   $��Rapor Parametreleri��6$   .��5      �  � ColumnDefinitions 5      �� 5      $��* �6'   �� 5      $
��Auto �6'   .+�5      �� $��1��6   $	��100��5      $��5q�5      $	��Top=�5      $��Raporu Çalıştır��5      ��5      �� *ExecuteReportCommand+5      �� 5!      $��3��6   $��Center=�5"       �� Orientation$ 
Horizontal=�5#      .+�5%      �� $��1��6    $��0��5&       $��Center=�5'       $��Peronel��5(       $   PDYS.UserControls.ExtendLookup  5*      $��1��6&   $��1��5+   &   $	��200��5,   &   $��3q�5-   &   $
��Left=�5.   &      LookupViewModel 5/   &   �� *LookupPersonal+   SelectedDataModel 50   &   �� *
Personal+ �� Mode$ TwoWay=� �� ValidatesOnDataErrors . �� 56      $��30,0,0,0q�6    $
��Left=�57       $��Center=�58          DarkFontColor#��[  59       $��Ay��5:       �� 5<      $	��120��6   $��10,0,0,0q�5=      $
��Left=�5>      $��Center=�5?       �� 
IsEditable .  ��5A      �� *	ListMonth+	 �� SelectedItem	 5B      �� *SelectMonth+$ TwoWay=� . �� 5F      $��30,0,0,0q�6    $
��Left=�5G       $��Center=�5H       #��[  5I       $
��Yıl��5J       �� 5L      $��80��6   $��	10,0,10,0q�5M      $
��Left=�5N      $��Center=�5O       .  ��5Q      �� *
ListYear+	 5R      �� *
SelectYear+$ TwoWay=� . 5X      5Z      5[      �� 5b   
   $��1��6   $��0,0,0,0q�5c      #��[  5d      $��1,1,1,1q�5e      
 �� Padding$
 0q�5f      ._�5g      -  'DevExpress.Xpf.Printing.DocumentPreview     Model 6"   �� *PreviewModel+5h      5i      5j      