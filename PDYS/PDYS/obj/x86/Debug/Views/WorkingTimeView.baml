   M S B A M L   `   `   `  ����  PDYS     PDYS.Views.WorkingTimeView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      b ^DevExpress.Xpf.Core.v11.2, Version=11.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      .��5	      ��   �� Orientation$  
Horizontal=�6   .+�5   
   �� $��0,0,5,0q�6   $��Center=�5      $
��Saat��5      %  DevExpress.Xpf.Editors.TextEdit  5   
   $��60��6   $��0,3,0,3q�5      $
��Left=�5      $��Center=�5         Mask$ HH:mm��5         MaskAllowNullInput .     MaskIgnoreBlank .     MaskType$ DateTime=�5         MaskUseAsDisplayFormat . %  DevExpress.Xpf.Editors.BaseEdit   	EditValue 5      �� *Time+ �� Mode$ TwoWay=� �� ValidatesOnDataErrors . {�5      �� *
IsEditable+$ TwoWay=�5      �� 5   
   $��5,0,5,0q�6   $��Center=�5      $
��Fark��5         5"   
   $��50��6   $��0,3,0,3q�5#      {�.   5$      �� *
Difference+$ TwoWay=� . 5'      �� 5*   
   $��5,0,0,0q�6   $��Center=�5+      {�.  	 d� 	IsChecked	 5,      �� *	IsValid+$ TwoWay=� . 5/      �� 51   
   $��2,0,0,0q�6   $��Center=�52      $��Doğru��53      56      57      