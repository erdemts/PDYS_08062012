   M S B A M L   `   `   `  ����K  GPDYS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b541947c23086f5b5clr-namespace:PDYS.UserControlsPDYS.UserControls  5      $    PDYS.Views.EmployeeHolidayView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      b ^DevExpress.Xpf.Core.v11.2, Version=11.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      (ucclr-namespace:PDYS.UserControls  5      $	��400��5      $	��250��5	      .��5      �   � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��10 �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��5      $��Center=�5       $��Personel��5!      $   PDYS.UserControls.ExtendLookup  5#   
   $��0��6   $��2��5$      $��3q�5%         LookupViewModel 5&      �� *LookupEmployee+   SelectedDataModel 5'      �� *
Employee+ �� Mode$ TwoWay=� �� ValidatesOnDataErrors . �� 5+   
   $��1��6   $��0��5,      $��Center=�5-      $��
İzin Tipi��5.      �� 50   
   $��1��6   $	��200��51      $
��Left=�52      $��2��53       �� 
IsEditable .  ��55      �� *ListHolidayType+ �� SelectedItem 56      �� *SelectedHolidayType+$ TwoWay=� . �� 5:   
   $��2��6   $��0��5;      $��Center=�5<      $��Başlangıç Tarihi��5=      %  DevExpress.Xpf.Editors.DateEdit  5@   
   $��2��6   $��2��5A      $	��150��5B      $��3q�5C      $
��Left=�5D      $��Center=�5E      %  DevExpress.Xpf.Editors.BaseEdit   	EditValue 5F      �� *	StartDate+$ TwoWay=� . �� 5J   
   $��3��6   $��0��5K      $��Center=�5L      $��Bitiş Tarihi��5M        5O   
   $��3��6   $��2��5P      $	��150��5Q      $��3q�5R      $
��Left=�5S      $��Center=�5T       5U      �� *	EndDate+$ TwoWay=� . �� 5Y   
   $��4��6   $��0��5Z      $��Center=�5[      $��Ücretsiz İzin��5\      �� 5^   
   $��4��6   $��2��5_      $��3q�5`      $��Center=�5a      $��3q�5e      	 d� 	IsChecked	 5b      �� *IsNotPayment+$ TwoWay=� . 5e      �� 5g   
   $��5��6   $��0��5h      $��3��5i      $��0,10,0,0q�5j      $��Stretch=�5k      $��
Açıklama��5l      .��5n      %  DevExpress.Xpf.Editors.TextEdit  $��3q�6   $��Stretch=�5o      $	��Top=�5p      )  #DevExpress.Xpf.Editors.TextEditBase
   AcceptsReturn
 .    TextWrapping$
 Wrap=�5u       5r      �� *Description+$ TwoWay=� . 5u      5v      5x      5{      