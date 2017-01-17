using Xunit;

using Program = hhs_p6_webshop_project.Program;

namespace hhs_p6_webshop_project_test {
    public class ProgramTests {

        [Fact]
        public void ShouldHaveAppName() {
            // Application name should not be empty
            Assert.NotEmpty(Program.APP_NAME);
        }

        [Fact]
        public void ShouldHaveVersionName() {
            // Application name should not be empty
            Assert.NotEmpty(Program.APP_VERSION_NAME);
        }

        [Fact]
        public void ShouldHaveVersionCode() {
            // Application version code should be specified and above zero
            Assert.True(Program.APP_VERSION_CODE > 0);
        }

    }
}
