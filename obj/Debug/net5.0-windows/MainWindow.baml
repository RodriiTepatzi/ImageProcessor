   M S B A M L   `   `   `  ����M  IProcesadorImagenes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:ProcesadorImagenesProcesadorImagenes  5   	   M+clr-namespace:ProcesadorImagenes.ViewModelsProcesadorImagenes.ViewModels  5   	   #    ProcesadorImagenes.MainWindow   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:ProcesadorImagenes     5   	   :vm+clr-namespace:ProcesadorImagenes.ViewModels     5   	     � Title$  
MainWindow��5	   	   $	��450��6   $	��800��6)    � WindowStartupLocation$ CenterScreen=�65    � SizeToContent$ Manual=�6Z    � WindowStyle$ SingleBorderWindow=�6q    � 
ResizeMode$ NoResize=�6�    � DataContext 5
      7   1ProcesadorImagenes.ViewModels.MainWindowViewModel  5   
   .��5      �  � ColumnDefinitions 5   
   �� 5      �� 5      .+�5   
   �� $��Cargar Imagen��6   $
��Left=�6)   $��
121,15,0,0q�6D   $	��Top=�6X   ���	#FF95CBD6
���   �$��	16,6,16,6q�6�   ���	#FFABF0FF��6p   �� *LoadButtonCommand+6�   �� 5   
   $��50,87,50,44q�6   .+�5      �� $	��250��66   $	��250��6C   ��6   �� *ImageInputPath+6C   �� 5      $��25��67   $	��192��6C   ��6   �� *ImageInputPath+ � ToolTip 6O   �� *ImageInputPath+5      �� 5   
   $
��Gris��6   $��1��6    $
��Left=�60   $��
127,15,0,0q�6K   $	��Top=�6_   $
��Gris��6�   $��	16,6,16,6q�6�   ���	#FFABF0FF���	#FF95CBD6$��1,1,1,1q�6�   ��6w   �� *ConvertButtonCommand+6�   �� 5   
   $��Negativo��6   $��1��6$   $
��Left=�64   $��
200,15,0,0q�6O   $	��Top=�6c   $��Negativo��6�   $��	16,6,16,6q�6�   ���	#FF95CBD6���	#FFABF0FF��6{   �� *ConvertButtonCommand+6�   �� 5   
   $��1��6   $��28,87,28,44q�6%   .+�5      �� $	��250��67   $	��250��6C   ��6   �� *ImageOutputPath+6C   �� 5      $��25��68   $	��192��6D   ��6   �� *ImageOutputPath+ 6P   �� *ImageOutputPath+5      5      5      