# HtmlParseTests

## Instructions for use
- Copy the SampleText.txt to a directory of your choice.
- Set the command arguments accordingly.

## Running the tests
- Change the text file in a couple of ways to see how the different components react to different text types.

## Conclusion
- When having a text containing the tag the components are looking for, all three components are more or less equally fast.
- When having a text lacking the tag, CsQuery seems to break down and spends a crap load of time trying desperately to find a matching tag. The other two components are more or less equally fast without the matching tag.
