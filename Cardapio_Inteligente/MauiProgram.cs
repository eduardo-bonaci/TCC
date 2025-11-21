using Microsoft.Extensions.Logging;
using Cardapio_Inteligente.Servicos;

namespace Cardapio_Inteligente
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // ✅ NOVA ARQUITETURA: Registra implementação baseada na plataforma
#if ANDROID
            // Android: IA local embutida no app (não precisa de servidor)
            Console.WriteLine("📱 Plataforma Android detectada - usando IA local embutida");
            builder.Services.AddSingleton<ILlamaService, LlamaServiceLocal>();
#elif WINDOWS
            // Windows: API embutida rodando localmente no mesmo processo
            Console.WriteLine("🖥️ Plataforma Windows detectada - iniciando API embutida");
            
            // ✅ Registra o serviço que roda a API internamente
            builder.Services.AddHostedService<ApiHostedService>();
            
            // ✅ Cliente que se conecta à API local (localhost:5068)
            builder.Services.AddSingleton<ILlamaService>(sp => new LlamaServiceRemote("http://localhost:5068"));
#else
            // Outras plataformas: API remota
            Console.WriteLine("💻 Plataforma Desktop detectada - usando API localhost");
            builder.Services.AddSingleton<ILlamaService>(sp => new LlamaServiceRemote("http://localhost:5068"));
#endif

            // ✅ Registro do serviço HTTP (para pratos, usuários, etc.)
            builder.Services.AddSingleton<ApiService>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            var app = builder.Build();

            // ✅ Verificação de inicialização baseada na plataforma
#if WINDOWS
            Task.Run(async () =>
            {
                // Aguarda a API interna iniciar
                await Task.Delay(3000);
                
                try
                {
                    var llamaService = app.Services.GetRequiredService<ILlamaService>();
                    var isReady = await llamaService.IsReadyAsync();
                    
                    if (isReady)
                    {
                        Console.WriteLine("✅ API interna está rodando em http://localhost:5068");
                        Console.WriteLine("✅ Aplicativo pronto para uso!");
                    }
                    else
                    {
                        Console.WriteLine("⚠️ API ainda está inicializando...");
                        Console.WriteLine("ℹ️ Aguarde alguns segundos e tente novamente");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Erro ao verificar API: {ex.Message}");
                }
            });
#elif ANDROID
            Task.Run(async () =>
            {
                await Task.Delay(2000);
                
                try
                {
                    Console.WriteLine("🤖 Inicializando IA local no Android...");
                    Console.WriteLine("⏳ Isso pode levar alguns segundos na primeira vez...");
                    
                    var llamaService = app.Services.GetRequiredService<ILlamaService>();
                    var isReady = await llamaService.IsReadyAsync();
                    
                    if (isReady)
                    {
                        Console.WriteLine("✅ IA local carregada e pronta para uso!");
                    }
                    else
                    {
                        Console.WriteLine("❌ Falha ao carregar IA local");
                        Console.WriteLine("ℹ️ Verifique se o modelo .gguf está presente nos assets do app");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Erro ao inicializar IA: {ex.Message}");
                }
            });
#endif

            return app;
        }
    }
}
