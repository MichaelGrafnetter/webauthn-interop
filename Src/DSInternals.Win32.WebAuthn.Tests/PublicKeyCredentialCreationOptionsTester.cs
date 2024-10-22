using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSInternals.Win32.WebAuthn.Tests
{
    [TestClass]
    public class PublicKeyCredentialCreationOptionsTester
    {
        [TestMethod]
        public void EntraIdPublicKeyCredentialCreationOptions_Deserialize()
        {
            var options = JsonSerializer.Deserialize<PublicKeyCredentialCreationOptions>(@"{
                ""rp"": {
                    ""id"": ""login.microsoft.com"",
                    ""name"": ""Microsoft""
                },
                ""user"": {
                    ""id"": ""T0Y6mho3vwFprE6aslBq9b2Qjz7Ixbn0PntEPkHirHimyfAtmz4xoilu6sNoiPFkmvxW"",
                    ""name"": ""john@contoso.com"",
                    ""displayName"": ""John Doe""
                },
                ""challenge"": ""ZXlKaGJHY2lPaUpTVTBFdFQwRkZVQzB5TlRZaUxDSmxibU1pT2lKQk1qVTJSME5OSWl3aWVEVmpJanBiSWsxSlNVUmhSRU5EUVd4RFowRjNTVUpCWjBsUmJ6bHBiMVZwVVhadk4wcFFPVmhCUWxCUFJsQTFha0ZPUW1kcmNXaHJhVWM1ZHpCQ1FWRnpSa0ZFUWpSTldGbDNSVkZaUzBOYVNXMXBXbEI1VEVkUlFrZFNXVVJpYlZZd1RVSlZSME5uYlZOS2IyMVVPR2w0YTBGU2ExZENNMlJ3WW0xU2RtUXpUWGRJVVZsRVZsRlJSRVY0V2s1VmVURlFZMjFrYUdKdGJEWlpXRkp3WWpJMGRGRlhUbXBhV0U1NlRVTnpSMEV4VlVWRGVFMXJUMFJLYTFsdFJtcFpWRkYwVFRKVk5FMVRNREJPYlU1b1RGUnNhazU2VFhSTlJHc3hUVWROZUZwWFJtcFpWR3N6VFVJMFdFUlVSVEJOUkZGNFQwUkJNRTFVUVRGTmJHOVlSRlJKTUUxRVVYaE9WRUV3VFZSVk1VMXNiM2RNZWtWMFRVTnpSMEV4VlVWQmVFMXJXVzFLYUU1cVdUVk5WMVYwVG1wck1rOVRNREJQVkd4clRGZEthMDlIUlhSTmFsRXlXbFJCZDAxdFZtcE5lbWN3VFVsSlFrbHFRVTVDWjJ0eGFHdHBSemwzTUVKQlVVVkdRVUZQUTBGUk9FRk5TVWxDUTJkTFEwRlJSVUZyUVRkWmVFSkdPV1pwV0VWaWN6UkVaM00yTlV0WWFTdHBjbEUxT0VsMVJrazJWbTR5VWs0eGMzZHRkMGRpTjJwWk1uTXhjVk13TkV0blUyTmpWSFZDVkdoWVUwaEZORFJpYzJGQmIwUlNPRFV5U1ZOdE1sbFhjVkIyUVZwUmFTdFhTbEpTYWxsWU1tazFSWFZUWm1SRGRHTXZZMVk0VGxSV2NuaFhNMVpRWVZScmEzRm9WR1ZaUWxGVFJHWjRjbGd4ZEZGM09VWTJRbUZIU3preWIwRXZWU3RPTmxGMmRVNXNlRmRhVWpOSVUyOW9ZM2w1WmtWSk1FWlhiemgxVkVsaWRqbHpUbWszYVVGT09FbENTMloxVnk4NFJtcE5ZbmhaWjFaWFJFZ3daSFZNUjBKMk5rczVhRll2WW1NMlluQjNWMlpKZDJaR2FFZFVOMWxNYUZaMk5rVjJkbkE0YmpGWlVIUTVTR2gzYlRnM2FYUlpSRzFHTlZGVk4ySjFaV0phVFhKaVUybE9NR2RUVkhCSFIyWkxNRGx4VUV4TWVFODVVbUZ2Ym5BeGMxQk9RV3h2Tkhod1ltSk5jV1puWTJGTk16RlFTRkZKUkVGUlFVSnZlbU4zVGxSQlRVSm5UbFpJVWsxQ1FXWTRSVUZxUVVGTlFtZEhRVEZWWkVwUlJVSXZkMUZQVFVGM1IwTnBjMGRCVVZGQ1oycGpWVUZuUlhkRGQxbEVWbEl3VUVKQlVVUkJaMDAwVFVFd1IwTlRjVWRUU1dJelJGRkZRa04zVlVGQk5FbENRVkZDTXpjcllWcGlWMUJGUmxKNmFYQlBSVU0yZGtsdVVXa3lSbmgyWTFGb1duaFNNWEZhUmtwSFNtRldSM2wxU1dseGVFNXJjbmwzU0RReVQwZHdlR3RFTTNKdEszcDNaRVY1ZVU5UFYwazJXSEJzVlU1NWFGTjZWM0ZXSzBSM2RIcGlkWEJVVDJaQ1NERmhLMnRSVkhnMWMwYzJabXBWYTBJeWFtbElZVEIxWVZvMFl6SXdNV0pzUmtsdWVuTnlVMkpoWldsemFscDNOM1pxT0dSa1JGcG5jazB4Tm5GWWFrTkJOelZuWTJSRGNWcE5XVTk0Y205YVpuQkVURTl5Tm5sc1kwVkJWbVJGUlV4RVoxRkVUM1l4YVVOQ2VEazFVMmRrWXpOUU55OWhXWHA2VFZvd1ZWQXJjMEZHY0Zvd09GSkRZWFZIVVhKaE5sTjRNbVJRV0dnM1ZWUTJObGhYVVZKNVJUVkVWV2RuZW5KcFRIRTJiRGhOVFdoUmIxZzBkMkpuV1ROTFJXWnpTWHBMVDNkdEswZ3hWaTh6V1RkdFJ6Sk9VU3RQV1ZwbkwxQk5PVkpJVUVSSlNWRlVVemh4TjB4ek1HOUZXVWNpWFN3aWVEVjBJam9pTTBNMFEwSTVOVVV4TWpJMU5VUkVRVGt6TmpNMk1qQTROa0pFTVRNNU16WTJRak5GT1RaQlFTSjkuTFBWTFpJeGdqc2hXRVQyd1dueG40bmZremJxZXdiN3VSamlfOXotMzNLWnpBTEF0QXdhNE40NnRoLVVEdXpOVlp4c0RtYkpjOWFJaUU3ZDRXdlpOYjg0LUtDbVJfTWJFYlFCYjlINU5ldXFycnktaWhoNzctNWxRVEgxSDNCVjFMMl8yUTEweUgyQlpkUVhuZTQ5UlM2QVY4SVBIYUUyT1ZFS2lxcjZSY0Z3eGUzSG5ZdFBpQUlST0dzSkstVnBkQ21RTm9fWWttTS1Dd2dPNVZtMnhDM2FnS1RkYWQxVmEwb0s4djZVQlZaSDJqSW5za21DWUhtYkplbmhqcGRFVjhGOHM3UGZ0VWxEQWlycHpxdnNqdkpIX2x3bkxjaW9aa0Z6V052ZFJYMk81ZzBPTGhMSkUwbXZmSDB1ZHVKMi1OdGtGQzFvRTZ1Vi0xMjY0MUNGWFVnLkN0NkdtbWtGS1BxR1lELVBSV04yd2cuVC10OEpibEtocmtRUDRRSHVrSlc2NDRmbFFKVjc1c1VIYXQweDlPSjVQNERXZTRHUU95US03bWMzcVBESWJQX2hyb25jMXhCbFBsSmt1ay1xV1pONUIxOXNJUWhPWVo3S0JfbktkQktDb0xIY21idmZpc0hSLXowRkdySlFUUkRzSmhVQVVSNHNKYm5vODBjU240NTYwN2FNOGxBTFJSNkJERkIzVkxxaW1Yd19ROGJiWHo1UTNzS1R5QTFaa0UtRGxuUFBGbm5pOTNuekhCZlQ4clVFcmctb1lPeTNMTWtiUk1qYlVDWDk5eW9jYmc5RG9Ba3hNQ04zcmZac2hyUVdvTC1FOUVTd2w2YkVlZlByb3pTNXozOENTTVJVZlIzUXNtMkxyVi1QUl9HOTFLUGpIeUtreG52UTNGdWpPUDJMOFR3bkpRWk9lUEtjVV9GSUYxWjFFZjV2TVBVTF94QzgzRWNvenE2b2JWRERDZl9aVlpueEh6bnJndy1YSjlpTC0tVVBiOHFjVE55eFl1bkhsR3ZoV2c3V2NuV09rZkJzbGlWZ0MxM01JeEZWRFQwTm9ta2pWb3R1cjdPenZtMDFienpicS0wMG1vdlJvUnlTSDFKcmJDcUdfN2tVOElGcG02Q3Q3ZGVZUlE1QjgySVRQZ0RMZllEOTNtUHdvQjh5eGhPSklMeThCa2NvbkZCd3VkZWJwWmdQaGJwQ1U3UDI3NVo5R0xDTkEtMjRSd190Sl8xb3NUcm5LSFB3bHpmR3VRVTE0RXpUaHotRVNjamZHLWVERXFDeGtINXk2WjV6ZFg4MW5WQzliLWppN3FHdGc4SVdLaklQamU0LWhueTM0ZWNXUndVVjM5NWEwZzVUWi1sNlNVSG1hVmNDdl9GQWNWUmlRaWxBMXFibl80UlotZW92NEpySUlOZE44a2NOZTJHV1hBMEVjU0xRVkdYUl9sbDBDNFlsdmZaeE53NEpyZ0pFU192VE1vQWZWM2JDdGVCcC00cTVZTm1tRm5qSUtlVUhwOWUxdi1tYnBZNXp1WnVxYTVhcWlOQU5sbnozUWdKVS1HbUlmeS1zQ01PdzJSTVJpYld4T0NmN0JmNGtDTUxHR29Rdl9HLVVPTEtyZjBxWHE3NzRJamV5bmpyYVdLQjRndXV3Q1oxWnpKUUwzRXZ5dlZTNF85NVhEWmtsaWh6N20zaGtNeUVMbHRCMDA2SUh0YnB1MmQ4MXI3VkYyQWUyWThPRTRsdHVtUlVxSUhSakZYQi16cTRWd0VoeXB1a2xWVExwVVJyT09FQkhKWWVMdUU2SnBXOFF6SHhFTl9YWWUycnh1VllFRU5KeklNMUh3bVhLcmN0cGZlZ19DeE9fZmJGbjlmN0FJVTlCMFFqaWdwUEVOMXpNbWJERG1rV01WV2NTbXEtNklVYkRkNEQ2OEt4LW5weEJrVEo4YnRLYlV3dUlJY25hSEliRVk0VkE2bjVpWE1OX2tHM3FlaHhtRklEVUNTdEhkT1l1dWF0ZlhSUjZYWmVJLU5YelBld205N2NLN2JLYmJGdE9RUUlLeFJxUzZSR2xMN0tjVGYyWHdaUkx1bGpWZ1gtdVpQNzJDVXloNG10TnU4allvWmVwaThGQnpTQWtlZS11SmV2UHMyMDVPd3I3d29wWnJnS3hpNVp3UjNOVWltVmVmZEE1U3dsal9LSWVydEpPYTZRbTRiSElMQW81dU5hNEtYcDNxQU96ekJaSVpSVWFPSEpyYnB5aUFBU24zOWFGRE91bExsM3BsNkkzZ2Rxc0lIcFNkczROUXFyNlJ6WVB0NzBxQ1hnWGpjRnlFaE91NDdES2NMenVBeTgtcWotQ2paWVBCUWpfcUNHUUJiYTh2eWtzMDBHMU12RlJpam1wRzFaT0d4UDhyZWxvRXdqLS13TEtLQnlkdFhkYUw4RndrLTZGUkRBU21EUGZLQUl5SGppS3owaHN6NERueUR0X0RKaFd5RnNXVEFjTVpIdE03R2hnU0dKa1A3SVVRcFUyN3JYdGlDTm11ajlKN0JDcEpPdmtNdHhuTmgzejc5QklneHRxTkJnOXV6MDJnUllWVDdUTHN4c1pjaEZmOFZQNExhQTZpbDNvanpBSFhWLUUzd3lrWTEzcmo2OURUTTg3bUxzY1dlTnV1MkpUNkl5MEplRGdNU25WVzM3ZkRPMEtldV9Fa1Uwa0hRSU1TU0NpdnlyeFNLMlJnRmVBelFNNndLOHEyYXl2ZVRNamZzcXRib3ZubUlmRjh6TkJESWM0VGhvdGFGRjYwTmx5NkRQMjAyRktFd2VYV0k2dnFtUHVVcElFQzlJS2pHSU5kR0dCRjVuOHRUMVNIRTdKR2l4MFdMc1ZlN0NSQkdwMU1DZFZ1QkFkalRxSFZybXZKWUgtbkZfUWFuZS1wQ181ZW5tcmszdHhoZVp5WXJha3BwRTZrNEhWaThPQXRkYmc2S3lGUDVTX0Y4YWdEUmFkek5IdGE0MVRXb3ZKSjZwbFZyZW5CLTcya0h0ME0zc2dqUXlOQU8zMkhUQnNPZXlOR1F6WkNfRkdaN3EwYWRhWU51Nm5DMHNnMHNzWVdxVS1ieGV1ME1RWHlkUzBtZFFMUmRTUFdnM0ZXYmJhM3pmUkRrQ295UXZhdkE2dXdlTXFfQUJwMnJxVWM3RmlNREV6VGVFcjFVOERRTWNLam8wb0paRDYtMUM0NlN5LWlDckhCcFlYS2txUmg5TW8tVWs3Nzl2TlF4YWc2TmtwMWRfa3k3WTRPbG5ncm1TenFoM0tqUjZ3OTByRUt1LVlQM1ZkR3hlUnkzd2RCNU1LUUFmMlZfUGJjNlpZc2JkZVUydDkzUkpsYmlaQ2hWa3NCNzc3b2hVWDk2cnUzOGhscjZGSU9WMk5YMHA2X3FQb19HNnZxNW5VQkh3MU9DOWpLQUlFRXNzaGcwdVdKMEpVUGhfTlNQMlQ2YnFRZWZKNTYyRlZoVlFIMkVIbUFvMWlrbFRDRFk3M0hKcUE5S2NZNW9sQXEyT2Q5eVBPcXk5SU40ajRReWRvTmhKTzVVY1FaYmNPZUF4N2kzT3pBWlVzYTRuNDlmNlh3bFRyVWRBRXNwT3dmU0NwMHR2Sl9FSG8zOGtpLWlKWnpkVzhiajVnT1Z2NVpBRHlLeWtCZ3lXZUFJN0FiaGdkWkJhYjRMaFRiY0c5dEdUTzBhbjhuVDkxUHViTENjbjltQ05DM01EdWpmYmktMmR3WFBnSGdCTmtWd3JJcTFsUGFLZlpuUXhaWGh0aDFGYkxDU1A1UklkTmh3eF85N05XdVUtSEozR29rNllRQnFpQnJ3NGRFbjBhc2pRLVplYVZFY2pyY3NuNURRRFd2a19QRXFLTUpBcjN5QURxOEFibHJQelRZVGFGdy1TczZ2TXdKQ1hoTTRoRXJYOTB0Mm84RGQ1dVN6TTFCNExUTUhyTVBiaktjY2Q0UHcwOVNMQW5uNGJIRm5CU0dzYVVMMVlMZ3VkdllQTEpFc3RIanlpQXcyTXk5NHJTbUg4Q2NmdC1UVllDQjZxZGEtNGtqTTFFbnQzNG5BSjREYlM4SklCTXlkWUZndF93WHBwUTBkNFUwTmtZdFBUTlV1dnlZMkJrVUVvOWs0aTRZRXlUMGFJRlY4SGd4SGRxU1ZEbW9JRGFkRlN6emcxQ28xVVh3MkpRQllRWE1WY05pcU1VRWVuS0EtR214TzlWSHVFNWp3aTgzWk1OYmN2a1JxdTF5cHpJYktoMXZIbDVfdXhEN0lZaGsxRGR5bG91bWRkT0FBX3Jjei1GMDAydF9sXzJ1SFFVOVcxNUFwVDl0X25oZmwxZmxRalRidGZhd0hEQWxCZjFLRFFiZV93QVd0eUNoXzNtZjRoUGdpQmlqbTZKdTh5SnFKcHpfUEgzX1c0LWlRMXZnQUoxTEV5VTZaMXZJYkJPelBTUXVmQ1lrTU1UV0ppZ3pZbm9raS1SeXlKWWMwRTg1ZU1pUDFmVWtDTTdWbkJiYnU2SU5ERV9OOFo3cV9CRVlwaC1xNmtENm9uanhRUnpKcEJfQS0yOGVNLVFqSkYwTl9jLUZzZHRTcDBXaDM3OGhoSkhkMnRyUlZOWnNWbXB3VGZXWHhjbHl3SkpnclFpVGNhSEtvRXNJUmtFdHl0LlB6NktCcDV0Y0VwNC12LWUxemt4c1E"",
                ""pubKeyCredParams"": [
                    {
                        ""type"": ""public-key"",
                        ""alg"": -7
                    },
                    {
                        ""type"": ""public-key"",
                        ""alg"": -257
                    },
                    {
                        ""type"": ""public-key"",
                        ""alg"": -8
                    }
                ],
                ""timeout"": 60000,
                ""excludeCredentials"": [
                    {
                        ""type"": ""public-key"",
                        ""id"": ""TZ2KrDEawbrIGQsnheGZ/BG9Nfnb7blSGwQYMGyJIhM="",
                        ""transports"":[]
                    },
                    {
                        ""type"": ""public-key"",
                        ""id"": ""g/V3JMewxqZaKD8mRpB2pTTl3SBnOR6gZEw/URDlB4o="",
                        ""transports"":[ ""usb"" ]
                    },
                    {
                        ""type"": ""public-key"",
                        ""id"": ""GshyINLMaOOwqt1LNUi0gQ=="",
                        ""transports"":[ ""usb"", ""nfc"" ]
                    },
                    {
                        ""type"": ""public-key"",
                        ""id"": ""9ccVagkzE02vPo+qoonYDv8FoREd9QYOn8RFSsbLr8Y="",
                        ""transports"":[]
                    }
                ],
                ""authenticatorSelection"": {
                    ""authenticatorAttachment"": ""cross-platform"",
                    ""requireResidentKey"": true,
                    ""userVerification"": ""required""
                },
                ""attestation"": ""direct"",
                ""extensions"": {
                    ""hmacCreateSecret"":true,
                    ""enforceCredentialProtectionPolicy"":true,
                    ""credentialProtectionPolicy"":""userVerificationOptional""
                }
            }");

            Assert.AreEqual("login.microsoft.com", options.RelyingParty.Id);
            Assert.AreEqual("john@contoso.com", options.User.Name);
            Assert.AreEqual(AttestationConveyancePreference.Direct, options.Attestation);
            Assert.IsNotNull(options.ExcludeCredentials);
            Assert.IsTrue(options.AuthenticatorSelection.RequireResidentKey);
            Assert.AreEqual(AuthenticatorAttachment.CrossPlatform, options.AuthenticatorSelection.AuthenticatorAttachment);
            Assert.AreEqual(UserVerificationRequirement.Required, options.AuthenticatorSelection.UserVerificationRequirement);
            Assert.AreEqual(3, options.PublicKeyCredentialParameters.Count);
            Assert.AreEqual(COSE.Algorithm.ES256, options.PublicKeyCredentialParameters[0].Algorithm);
            Assert.AreEqual(COSE.Algorithm.RS256, options.PublicKeyCredentialParameters[1].Algorithm);
            Assert.AreEqual(COSE.Algorithm.EdDSA, options.PublicKeyCredentialParameters[2].Algorithm);
        }

        [TestMethod]
        public void OktaPublicKeyCredentialCreationOptions_Deserialize()
        {
            var options = JsonSerializer.Deserialize<PublicKeyCredentialCreationOptions>(@"{
                ""rp"": {
                    ""name"": ""Okta Tenant Name -- Environment"",
                    ""id"": ""example.okta.com""
                },
                ""user"": {
                    ""displayName"": ""Okta Doe"",
                    ""name"": ""okta@contoso.com"",
                    ""id"": ""00eDuihq64pgP1gVD0x7""
                },
                ""pubKeyCredParams"": [
                    {
                        ""type"": ""public-key"",
                        ""alg"": -7
                    },
                    {
                        ""type"": ""public-key"",
                        ""alg"": -257
                    }
                ],
                ""challenge"": ""AVun-poGmJKZOAT0r-KBSs-94BPqMf3j"",
                ""attestation"": ""direct"",
                ""authenticatorSelection"": {
                    ""userVerification"": ""required"",
                    ""requireResidentKey"": false
                },
                ""u2fParams"": {
                    ""appid"": ""https://example.okta.com""
                },
                ""excludeCredentials"": [
                    {
                        ""type"": ""public-key"",
                        ""id"": ""VX_AlCL9qUx2ox_Ekth4NYngvwpUswaBqcfb4XsHglI""
                    },
                    {
                        ""type"": ""public-key"",
                        ""id"": ""kFPT5CL3-I30e22QQ0WYo4C9EFCTcbWM0-G-wTBslVNzKzf-FsJ1CBVrgN2k5RJH2dTFJxyzgI06XxIbrcbpAA""
                    }
                ]
            }");

            Assert.AreEqual("example.okta.com", options.RelyingParty.Id);
            Assert.AreEqual("okta@contoso.com", options.User.Name);
            Assert.AreEqual(AttestationConveyancePreference.Direct, options.Attestation);
            Assert.IsNotNull(options.ExcludeCredentials);
            Assert.IsFalse(options.AuthenticatorSelection.RequireResidentKey);
            Assert.AreEqual(AuthenticatorAttachment.Any, options.AuthenticatorSelection.AuthenticatorAttachment);
            Assert.AreEqual(UserVerificationRequirement.Required, options.AuthenticatorSelection.UserVerificationRequirement);
            Assert.AreEqual(2, options.PublicKeyCredentialParameters.Count);
            Assert.AreEqual(COSE.Algorithm.ES256, options.PublicKeyCredentialParameters[0].Algorithm);
            Assert.AreEqual(COSE.Algorithm.RS256, options.PublicKeyCredentialParameters[1].Algorithm);
        }

    }
}
