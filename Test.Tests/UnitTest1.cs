using System;
using System.Threading.Tasks;
using Xunit;

namespace Test.Tests
{
    public class ProductsTest : IClassFixture<InjectionFixture>
    {
        private readonly InjectionFixture injection;
        #region Property  

        public ProductsTest(InjectionFixture injection)
        {
            this.injection = injection;
        }

        [Fact]
        public async void TestName()
        {
            // Given
        
            // When
        
            // Then
            var response = injection.client.GetAsync(new Uri("https://localhost:5001/products")).Result;

                response.EnsureSuccessStatusCode();
                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                        throw x.Exception;

                    Console.WriteLine(x.Result);
                });
        }
        #endregion
    }
}
