**Plotting Thingy**
_Prototype mortar plotting board training thingy for A.V._

Things to finish (in order of priority)

1. Vernier scale for measuring single mills
	+ There is a very precise relationship between the azimuth disk
	  and the vernier scale, otherwise it wouldn't be a proper scale.
	  Somehow have to figure out what that is...
2. Range labels on grid (50 m each line, labels every 500 m)
	+ UserCountrol (compound) of labels nested in canvas? or would the grid
	  lines go over top the labels?
3. Labelled markers - set label in sidebar and display adjacent to marker
	+ UserControl (compound) for label + ellipse + data
	+ rotate markers with azimuth disk the hard way (watch angle and do math)
4. Subclass canvas for drawing grid lines, draw grid lines on event of somekind
