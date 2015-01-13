**Plotting Thingy**
_Prototype mortar plotting board training thingy for A.V._

Notes and Ideas and Todos and Such:

- Instead of drawing grid lines when the app is initializes, they should be triggered by events (like load/initialize or redraw). Either:
    + Add event handlers to the existing canvas, OR
    + Subclass Canvas to encapsulate drawing logic (preferred?)
- Drag and Drop markers
    + Challenging.  Not sure how to do this yet.
    + Simple markers could just be dots w/ different colors
    + Drag from a dot in the left panel to a canvas overlaying the disk
    + On release, need to create a new marker on the canvas
        * If we want to add other action to the markers (labels, delete individual markers) then they can't just be painted, they need to be controls themselves that can be clicked!
- Make the plot area zoomable
    + No idea how to do this yet
