## Digital Plotting Board ##
_Prototype simulation of a mortar fire control plotting board, like an [M16/M19 plotting board](https://encrypted.google.com/search?hl=en&q=M19+plotting+board)_

### Dependencies

- [.NET Framework 4.0](http://www.microsoft.com/en-us/download/details.aspx?id=17718)

### Usage

Controls for rotating the azimuth disk, scaling ("zooming") the disk/grid, and placing markers are located in the sidebar at the left of the window.  

Rotation of the azimuth disk can be controlled using the slider or text boxes in the "Rotation" area of the sidebar.  There are separate text boxes for entering either mils or degrees, and the rotation will change as you type.  Additionally, the disk can be rotated using the keyboard:

- `Left arrow` : clockwise (`Shift` + `Left arrow` : faster)
- `Right arrow` : counter-clockwise (`Shift` + `Right arrow` : faster)
- `Shift` + `Up arrow` : reset to 0 mils

The entire grid area and azimuth disk can be scaled together using the slider in the "Scale" section of the sidebar.  This effectively provides a primitive "zoom" capability, allowing the size of the disk to be changed to show the whole disk or only a small part of the disk in the same area.  Scaling can also be controlled with keyboard shortcuts:

- `Ctrl` + `+/=` : increase scale factor / zoom in
- `Ctrl` + `_/-` : decrease scale factor / zoom out
- `Ctrl` + `0` : reset scale/zoom

Marker can be placed on the azimuth disk by dragging a color from the "Markers" area of the sidebar to a point on the disk.  After placing a marker on the disk, it can be dragged to a different point on the disk to reposition it or back to a color on the sidebar to remove it.  Clicking the "Clear All Markers" button will remove all markers from the azimuth disk.

### Incomplete/Known issues

- **[Bug]** Scaling immediately after running the app, before explicitly setting the size of the window, will cause the window size to change.
    + **[Workaround]** Adjust the window size (maximize, for instance) before scaling and the grid/disk will scale without changing the size of the window.
- **[Feature]** Editable marker labels in the sidebar
- **[Feature]** Labels attached to markers on the board
- **[Enhancement]** Proper CustomControl/UserControls extending Canvas for the grid and marker areas
- **[Enhancement]** Actual toolbar (maybe?)
- Learn how to use WPF "the right way" instead of just hacking at it

### Credits

- App Icon: http://icons8.com/