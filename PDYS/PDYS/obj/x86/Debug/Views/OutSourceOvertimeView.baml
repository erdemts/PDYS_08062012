   M S B A M L   `   `   `  ����K  GPDYS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b541947c23086f5b9!clr-namespace:PDYS.InfraStructurePDYS.InfraStructure  5      )clr-namespace:PDYS.ModelsPDYS.Models  5	      5clr-namespace:PDYS.UserControlsPDYS.UserControls  5
      &     PDYS.Views.OutSourceOvertimeView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      b ^DevExpress.Xpf.Core.v11.2, Version=11.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5      c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Cia:http://schemas.microsoft.com/expression/2010/interactivity  5      -infra!clr-namespace:PDYS.InfraStructure  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      %modelclr-namespace:PDYS.Models  5	      (ucclr-namespace:PDYS.UserControls  5
      $	��400��5      $	��300��5      .��5      �   � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��10 �6   �� 5      $��* �6   .+�5"   
   �� $��0��6   $��0��5#      $��Center=�5$      $��
Mesai Adı��5%      %  DevExpress.Xpf.Editors.TextEdit  5'   
   $��0��6   $��2��5(      $��3q�5)      %  DevExpress.Xpf.Editors.BaseEdit   	EditValue 5*      �� *Name+ �� Mode$ TwoWay=� �� ValidatesOnDataErrors . �� 5.   
   $��1��6   $��0��5/      $��Center=�50      $��Çalışma Saat Ücreti��51        53   
   $��1��6   $��2��54      $	��150��55      $��3q�56      $
��Left=�57      $��Center=�58      $��Right=�59         Mask$ N2��5=         MaskAllowNullInput .    MaskIgnoreBlank .     MaskType$ Numeric=�5@      	   MaskUseAsDisplayFormat	 .  5:      �� *HourlyPayment+$ TwoWay=� . 5A      �� 5E   
   $��2��6   $��0��5F      $��Center=�5G      $��Günlük Çalışma Saati��5H        5K   
   $��2��6   $��2��5L      $��60��5M      $��3q�5N      $
��Left=�5O      $��Center=�5P      $ HH:mm��5T       .   .  $ DateTime=�5W      	 .  5Q      �� *MaximumCharge+$ TwoWay=� . 5X      �� 5Z   
   $��3��6   $��0��5[      $��Center=�5\      $��	Gün Sonu��5]        5`   
   $��3��6   $��2��5a      $��60��5b      $��3q�5c      $
��Left=�5d      $��Center=�5e      $ HH:mm��5i       .   .  $ DateTime=�5l      	 .  5f      �� *DailyCheckPoint+$ TwoWay=� . 5m      �� 5r   
   $��4��6   $��0��5s      $��3��5t      $��0,10,0,0q�5u      $��Stretch=�5v      $��
Açıklama��5w      .��5y        $��6��6   $��2��5z      $��3q�5{      $��Stretch=�5|      $��Stretch=�5}      $	��Top=�5~      )  #DevExpress.Xpf.Editors.TextEditBase
   AcceptsReturn
 .    TextWrapping$
 Wrap=�5�      "   VerticalScrollBarVisibility$ Visible=�5�         Text 5�      �� *Description+$ TwoWay=� . 5�      5�      5�      5�      