# TaskBarFolder
This is a workaround for the Win 10 Taskbar folder menues that are missing in Win11:   
Display a folder in the taskbar notify-tray and add one or more directorys for quick access 
   

![image](https://github.com/user-attachments/assets/8fa73de8-3a19-4d30-9dc3-8a69581e69eb)
   
Left-Click opens either Single-Folder:    
![image](https://github.com/user-attachments/assets/da70caee-69c7-4320-a9b3-708ea6f153d2)
  
or Multiple-Folders:   
![image](https://github.com/user-attachments/assets/ab9e1edf-196a-4fbf-a4ee-1f253f359119)   
    
![image](https://github.com/user-attachments/assets/fac0608d-4d9c-43fb-b3e9-dcef8005cc24)   
  
right-click on folder or file opens the (old) windows context-menue   
double-click on folder opens the folder in windows-explorer  
double-click on file opens the file with the associated programm   
   
Right-Click on the task-bar icon for context-menue:    
![image](https://github.com/user-attachments/assets/a42bf6be-14b0-4e57-b76b-7afad4863467)  
  
Settings-Menue:    
![image](https://github.com/user-attachments/assets/727c04dc-32f8-4f3c-a75e-f5923c62b047)

  
You can change the default Task-Bar-Icon (reset with click on "default icon)   

Menue-Options:
- show tooltips with info about filetype,creation time, modification time and size
- show file extensions      
- show hidden files
- use small icons with 16x16 pixel - default is 20x20

- Add folder with "add folder" button
- change the order by selecting one row (by clicking on the row-header!) and moving it with the up/down arrows on the right side
- change the icon of the folder --> double-click on the folder icon
- reset the folder-icon to the default icon by selecting the icon and pressing "default icon"  
- change the display-name of the folder --> select the name, edit the text in the text-box below and press the "set name" button
- delete the folder by selecting it by clicking on the row-header and presseing " delete folder"
   
![image](https://github.com/user-attachments/assets/1b902d06-387a-4f34-ac80-366478f85848)

minimize/hide settings window by clicking on button top-right corner   

Issues:   

The first time the menue opens, opening subfolders may be very slow.  
This seems to be a windows problem, as this also was the case with the former windows 10 taskbar-folder menue  
This also happens when adding a folder to the quicklinks in the openShell start-menue   
Interestingly it seems not to be happening when using a root-folder like C:\   
Also in debug mode in visual studio it seems to work properly   
I was not yet able to figure out why this is the case... if anyone has an idea I would really appreciate if You could tell me!   
   
   
Hope this is useful!  
  
Cheers, Stephanowicz   
