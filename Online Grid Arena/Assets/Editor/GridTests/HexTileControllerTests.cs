using NSubstitute;
using NUnit.Framework;
using System.IO;

public class HexTileControllerTests
{
    IHexTileSelectionController hexTileSelectionController;
    IGridSelectionController gridSelectionController;

    [SetUp]
    public void Init()
    {
        hexTileSelectionController = Substitute.For<IHexTileSelectionController>();
        gridSelectionController = Substitute.For<IGridSelectionController>();
    }
}
