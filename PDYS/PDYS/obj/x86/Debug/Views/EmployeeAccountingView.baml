   M S B A M L   `   `   `  ����K  GPDYS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b541947c23086f5b)clr-namespace:PDYS.HelperPDYS.Helper  5      9!clr-namespace:PDYS.InfraStructurePDYS.InfraStructure  5	      5clr-namespace:PDYS.UserControlsPDYS.UserControls  5      '    !PDYS.Views.EmployeeAccountingView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      b ^DevExpress.Xpf.Core.v11.2, Version=11.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5      b ^DevExpress.Xpf.Grid.v11.2, Version=11.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49ag cDevExpress.Xpf.Grid.v11.2.Core, Version=11.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid   5      (helperclr-namespace:PDYS.Helper   5      c	 _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Cia:http://schemas.microsoft.com/expression/2010/interactivity 	 5      -infra!clr-namespace:PDYS.InfraStructure 	 5	      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	 5
      (ucclr-namespace:PDYS.UserControls 	 5      $	��400��5      $	��300��5      .��5      � $��1��6   {�6   �� *
IsEditable+  �� Mode$  TwoWay=� � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $	��100 �6   �� 5      $��10 �6   �� 5       $��* �6   .+�5$   
   �� $��0��6   $��0��5%      $��Center=�5&      $��Personel��5'      $   PDYS.UserControls.ExtendLookup  5)   
   $��0��6   $��2��5*      $��3q�5+         LookupViewModel 5,      �� *LookupEmployee+   SelectedDataModel 5-      �� *
Employee+$  TwoWay=� �� ValidatesOnDataErrors . �� 50   
   $��1��6   $��0��51      $��Center=�52      $��İşlem Tipi��53        55   
   $��1��6   $��2��56      $��3q�57       58      �� *LookupAccountingDefination+ 59      �� *AccountingDefination+$  TwoWay=� . �� 5=   
   $��2��6   $��0��5>      $��Center=�5?      $��İşlem Tanımı��5@      y�5A      �� *IsEditableSubect+   VisibilityConverter �� 	Converter# [  %  DevExpress.Xpf.Editors.TextEdit  5D   
   $��2��6   $��2��5E      $��3q�5F      %  DevExpress.Xpf.Editors.BaseEdit   	EditValue 5G      �� *	Subject+$  TwoWay=� . y�5J      �� *IsEditableSubect+# [  �� 5M   
   $��3��6   $��0��5N      $��Center=�5O      $��Borç��5P      y�5Q      �� *	IsDebit+# [    5T   
   $��3��6   $��2��5U      $��3q�5V      $��Center=�5W      $��Right=�5X         Mask$ N2��5\      	   MaskAllowNullInput	 . 
   MaskIgnoreBlank
 .     MaskType$ Numeric=�5_         MaskUseAsDisplayFormat .  5Y      �� *Debit+$  TwoWay=� . y�5a      �� *	IsDebit+# [  �� 5d   
   $��4��6   $��0��5e      $��Center=�5f      $��Alacak��5g      y�5h      �� *
IsCredit+# [    5k   
   $��4��6   $��2��5l      $��3q�5m      $��Center=�5n      $��Right=�5o      $ N2��5s      	 . 
 .  $ Numeric=�5v       .  5p      �� *Credit+$  TwoWay=� . y�5x      �� *
IsCredit+# [  �� 5{   
   $��5��6   $��0��5|      $��Center=�5}      $��İşlem Tarihi��5~      %  DevExpress.Xpf.Editors.DateEdit  5�   
   $��5��6   $��4��5�      $	��150��5�      $��3q�5�      $
��Left=�5�      $��Center=�5�       5�      �� *ProcessDate+$  TwoWay=� . �� 5�   
   $��6��6   $��0��5�      $��3��5�      $��0,10,0,0q�5�      $��Stretch=�5�      $��
Açıklama��5�      .��5�        $��3q�6   $��Stretch=�5�      $	��Top=�5�      )  #DevExpress.Xpf.Editors.TextEditBase   AcceptsReturn .    TextWrapping$
 Wrap=�5�       5�      �� *Description+$  TwoWay=� . 5�      5�      5�      5�      