   M S B A M L   `   `   `  ����K  GPDYS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b541947c23086f5b)clr-namespace:PDYS.HelperPDYS.Helper  5      9!clr-namespace:PDYS.InfraStructurePDYS.InfraStructure  5	      5clr-namespace:PDYS.UserControlsPDYS.UserControls  5          PDYS.Views.UserView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      b ^DevExpress.Xpf.Core.v11.2, Version=11.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5      b ^DevExpress.Xpf.Grid.v11.2, Version=11.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49ag cDevExpress.Xpf.Grid.v11.2.Core, Version=11.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid   5      (helperclr-namespace:PDYS.Helper   5      c	 _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Cia:http://schemas.microsoft.com/expression/2010/interactivity 	 5      -infra!clr-namespace:PDYS.InfraStructure 	 5	      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	 5
      (ucclr-namespace:PDYS.UserControls 	 5      $	��400��5      $	��300��5      .��5      � {�6   �� *
IsEditable+  �� Mode$  TwoWay=� � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $	��100 �6   �� 5      $��10 �6   �� 5       $��* �6   .+�5$   
   �� $��0��6   $��0��5%      $��Center=�5&      $��Tam Adı��5'      %  DevExpress.Xpf.Editors.TextEdit  5)   
   $��0��6   $��2��5*      $��3q�5+      $��Center=�5,      %  DevExpress.Xpf.Editors.BaseEdit   	EditValue 5-      �� *
FullName+$  TwoWay=� �� ValidatesOnDataErrors . �� 51   
   $��1��6   $��0��52      $��Center=�53      $��Kullnıcı Adı��54        56   
   $��1��6   $��2��57      $	��150��58      $��3q�59      $
��Left=�5:      $��Center=�5;       5<      �� *
UserName+$  TwoWay=� . {�5?      �� *IsEditableUserName+$  TwoWay=��� 5B   
   $��2��6   $��0��5C      $��Center=�5D      $��	E - Posta��5E        5G   
   $��2��6   $��2��5H      $��3q�5I      $��Center=�5J       5K      �� *Email+$  TwoWay=� . �� 5O   
   $��3��6   $��0��5P      $��Center=�5Q      $��Parola��5R      ,  &DevExpress.Xpf.Editors.PasswordBoxEdit  5T   
   $��3��6   $��2��5U      $	��130��5V      $��3q�5W      $
��Left=�5X       5Y      �� *
Password+$  TwoWay=� . {�5\      �� *IsEditablePassword+$  TwoWay=��� 5_   
   $��4��6   $��0��5`      $��Center=�5a      $��Parola Tekrar��5b        5d   
   $��4��6   $��2��5e      $	��130��5f      $��3q�5g      $
��Left=�5h       5i      �� *PasswordConfirm+$  TwoWay=� . {�5l      �� *IsEditablePassword+$  TwoWay=��� 5p   
   $��5��6   $��0��5q      $��Center=�5r      $��Giriş Hakkı��5s      �� 5u   
   $��5��6   $��2��5v      $��5q�5w      $
��Left=�5x       d� 	IsChecked 5y      �� *	IsLogon+$  TwoWay=�{�5{      �� *IsEditableLogon+$  TwoWay=�5�      5�      