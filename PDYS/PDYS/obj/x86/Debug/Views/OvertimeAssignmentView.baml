   M S B A M L   `   `   `  ����K  GPDYS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b541947c23086f5b)clr-namespace:PDYS.HelperPDYS.Helper  5      9!clr-namespace:PDYS.InfraStructurePDYS.InfraStructure  5	      5clr-namespace:PDYS.UserControlsPDYS.UserControls  5      '    !PDYS.Views.OvertimeAssignmentView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      b ^DevExpress.Xpf.Core.v11.2, Version=11.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5      b ^DevExpress.Xpf.Grid.v11.2, Version=11.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49ag cDevExpress.Xpf.Grid.v11.2.Core, Version=11.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid   5      (helperclr-namespace:PDYS.Helper   5      c	 _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Cia:http://schemas.microsoft.com/expression/2010/interactivity 	 5      -infra!clr-namespace:PDYS.InfraStructure 	 5	      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	 5
      (ucclr-namespace:PDYS.UserControls 	 5      $	��400��5      $	��300��5      .��5      � $��1��6     � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $��25 �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $	��100 �6   �� 5      $��10 �6   �� 5      $��* �6   .+�5"   
   �� $��0��6   $��0��5#      $��Center=�5$      $��Personel��5%      $   PDYS.UserControls.ExtendLookup  5'   
   $��0��6   $��2��5(      $��3q�5)         LookupViewModel 5*      �� *LookupEmployee+   SelectedDataModel 5+      �� *
Employee+ �� Mode$ TwoWay=� �� ValidatesOnDataErrors . �� 5/   
   $��1��6   $��0��50      $��Center=�51      $��
Mesai Tipi��52      �� 54   
   $��1��6   $��2��55      $��3q�56      $��Center=�57       �� Orientation$ 
Horizontal=�58      .+�59      	� $��Haftalık Mesai��6    d� 	IsChecked 63   �� *IsWeeklyOvertime+$ TwoWay=�	� 5:      $��20,0,0,0q�6   $��Kümulatif Mesai��5;       5<      �� *IsOutSourceOvertime+$ TwoWay=�5>      �� 5@   
   $��2��6   $��0��5A      $��Center=�5B      $��Kümulatif Mesai��5C      y�5D      �� *IsOutSourceOvertime+   VisibilityConverter �� 	Converter# [    5G   
   $��2��6   $��2��5H      $��3q�5I       5J      �� *LookupOutSourceOvertime+ 5K      �� *OutSourceOvertime+$ TwoWay=� . y�5N      �� *IsOutSourceOvertime+# [  �� 5R   
   $��3��6   $��0��5S      $��Center=�5T      $��Haftalık Mesai��5U      y�5V      �� *IsWeeklyOvertime+# [    5Y   
   $��3��6   $��2��5Z      $��3q�5[       5\      �� *LookupWeeklyOvertime+ 5]      �� *WeeklyOvertime+$ TwoWay=� . y�5`      �� *IsWeeklyOvertime+# [  �� 5e   
   $��4��6   $��0��5f      $��Center=�5g      $��Başlangıç Tarihi��5h      %  DevExpress.Xpf.Editors.DateEdit  5k   
   $��4��6   $��2��5l      $	��150��5m      $��3q�5n      $
��Left=�5o      $��Center=�5p      %  DevExpress.Xpf.Editors.BaseEdit	   	EditValue	 5q      �� *	StartDate+$ TwoWay=� . �� 5u   
   $��5��6   $��0��5v      $��Center=�5w      $��Bitiş Tarihi��5x        5z   
   $��5��6   $��2��5{      $	��150��5|      $��3q�5}      $
��Left=�5~      $��Center=�5      	 5�      �� *	EndDate+$ TwoWay=� . �� 5�   
   $��6��6   $��0��5�      $��3��5�      $��0,10,0,0q�5�      $��Stretch=�5�      $��
Açıklama��5�      .��5�      %  DevExpress.Xpf.Editors.TextEdit  $��6��6   $��2��5�      $��3q�5�      $��Stretch=�5�      $��Stretch=�5�      $	��Top=�5�      )  #DevExpress.Xpf.Editors.TextEditBase
   AcceptsReturn
 .    TextWrapping$
 Wrap=�5�      "   VerticalScrollBarVisibility$ Visible=�5�         Text 5�      �� *Description+$ TwoWay=� . 5�      5�      5�      5�      