//
// StageWriter.cs
//

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Galaxy
{
    public class CStageWriter
    {
        private static string Indent(int count, string s)
        {
            return new String(' ', count) + s;
        }

        private static string PrimitiveToString(object instance)
        {
            Type type = instance.GetType();
            if (type == typeof(float))
                return String.Format("{0:0.0#}f", (float)instance);
            if (type == typeof(bool))
                return instance.ToString().ToLower();
            return instance.ToString();
        }

        private static string PropertyToString(PropertyInfo property, Object instance)
        {
            StringBuilder sb = new StringBuilder();

            // TODO: either write raw type, or recurse into sub-type properties
            if (property.PropertyType.IsPrimitive)
            {
                object value = property.GetValue(instance, null);
                string primitive = PrimitiveToString(value);
                sb.AppendLine(String.Format("{0} = {1},", property.Name, primitive));
            }
            else if (property.PropertyType == typeof(System.Type))
            {
                sb.AppendLine(String.Format("{0} = typeof({1}),", property.Name, property.GetValue(instance, null).ToString()));
            }
            else if (property.PropertyType.IsArray)
            {
                object value = property.GetValue(instance, null);
                sb.AppendLine(String.Format("skip unhandled array: {0}", value.ToString()));
            }
            else if (property.PropertyType.IsGenericType)
            {
                // TODO: assuming sequence type!
                object value = property.GetValue(instance, null);
                bool b = property.PropertyType == typeof(List<>);
                IEnumerable enumerable = value as IEnumerable;
                if (enumerable != null)
                {
                    foreach (Object child in enumerable)
                    {
                        Type child_type = child.GetType();
                        sb.AppendLine(String.Format("new {0}() {{", child_type.ToString()));
                        foreach (PropertyInfo child_property in child_type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                        {
                            string properties = PropertyToString(child_property, child);
                            sb.AppendLine(properties);
                        }
                        sb.AppendLine("});");
                    }
                }
                else
                {
                    sb.AppendLine(String.Format("skip unknown Generic: {0}", value.ToString()));
                }
            }
            else if (property.PropertyType.IsClass)
            {
                Object child = property.GetValue(instance, null);

                sb.AppendLine(String.Format("{0} = new {1}() {{", property.Name, child.GetType().ToString()));
                string child_properties = PropertiesToString(child);
                sb.Append(child_properties);
                sb.AppendLine("},");
            }
            else if (property.PropertyType.IsValueType)
            {
                Object child = property.GetValue(instance, null);

                // TODO: dont need to write properties for value types?
                //string child_properties = PropertiesToString(child);
                //sb.Append(child_properties);

                sb.AppendLine(String.Format("{0} = new {1}() {{", property.Name, child.GetType().ToString()));
                string child_values = ValuesToString(child);
                sb.Append(child_values);
                sb.AppendLine("},");
            }
            else
            {
                sb.AppendLine(String.Format("*** {0} = {1},", property.Name, property.GetValue(instance, null).ToString()));
            }

            return sb.ToString();
        }

        private static string PropertiesToString(Object instance)
        {
            // TODO: base types!
            StringBuilder sb = new StringBuilder();
            Type type = instance.GetType();
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo property in properties)
            {
                string s = PropertyToString(property, instance);
                sb.Append(s);
            }
            return sb.ToString();
        }

        private static string ValuesToString(Object instance)
        {
            StringBuilder sb = new StringBuilder();
            Type type = instance.GetType();
            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
            foreach (FieldInfo field in fields)
            {
                object value = field.GetValue(instance);
                string primitive = PrimitiveToString(value);
                //string value = field.GetValue(instance).ToString();
                //if (field.FieldType == typeof(float))
                    //value += "f";
                //if (field.FieldType == typeof(bool))
                    //value = value.ToLower();

                sb.AppendLine(String.Format("{0} = {1},", field.Name, primitive));
            }
            
            return sb.ToString();
        }

        public static void Save(string filepath, CStageDefinition stage_definition)
        {
            StringBuilder sb = new StringBuilder(50 * 1024 * 1024);
            string stage_filename = filepath.Substring(filepath.LastIndexOf("\\") + 1);
            string stage = stage_filename.Substring(0, stage_filename.Length - 3);

            // header
            sb.AppendLine("//");
            sb.AppendLine(String.Format("// {0}.cs", stage));
            sb.AppendLine("//");

            // namespaces
            sb.AppendLine("namespace Galaxy {");
            sb.AppendLine("namespace Stages {");
            sb.AppendLine(String.Format("public class {0} {{", stage));

            // definition opening
            sb.AppendLine("public static CStageDefinition GenerateDefinition() {");
            sb.AppendLine(String.Format("CStageDefinition stage = new CStageDefinition(\"{0}\");", stage));
            sb.AppendLine("float StageTime = 0.0f;");

            // stage data
            foreach (KeyValuePair<int, List<CStageElement>> time_element in stage_definition.Elements)
            {
                int time = time_element.Key;
                foreach (CStageElement element in time_element.Value)
                {
                    Type type = element.GetType();
                    sb.AppendLine(String.Format("stage.AddElement({0}, new {1}() {{", time, type.ToString()));
                    string properties = PropertiesToString(element);
                    sb.AppendLine(properties);
                    sb.AppendLine("});");
                }

                sb.AppendLine("");
            }

            // end definition
            sb.AppendLine("return stage;");
            sb.AppendLine("}");

            // close namespaces
            sb.AppendLine("}");
            sb.AppendLine("} // namespace Stages");
            sb.AppendLine("} // namespace Galaxy");

            // write file
            TextWriter tw = new StreamWriter(filepath);
            tw.Write(sb.ToString());
            tw.Close();
            
            /*

//
// TestStage.cs
//

using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Galaxy
{
    namespace Stages
    {
        public class TestStage
        {
            public static CStageDefinition GenerateDefinition()
            {
                CStageDefinition stage = new CStageDefinition("TestStage");

                float StageTime = 0.0f;

                // asteroids
                StageTime += 2.0f;
                stage.AddElement(StageTime, new CSpawnerEntity {
                    Type = typeof(CAsteroid),
                    SpawnCount = 20,
                    SpawnPosition = new CSpawnPositionRandom(),
                    SpawnTimer = new CSpawnTimerRandom() { Frequency = 0.07f, IncreaseRate = 0.0f },
                    CustomElement = new CSpawnerCustomAsteroid(),
                });

                // Turret wave
                StageTime += 6.0f;
                stage.AddElement(StageTime, new CSpawnerEntity {
                    Type = typeof(CTurret),
                    SpawnCount = 3,
                    SpawnPosition = new CSpawnPositionFixed() { Position = new Vector2(350.0f, -100.0f) },
                    SpawnTimer = new CSpawnTimerInterval() { Delay = 1.0f },
                    CustomElement = new CSpawnerCustomMover() {
                        Mover = new CMoverSequence() {
                            Velocity = new List<Vector2>() {
                                new Vector2(0.0f, 2.0f),
                                new Vector2(-2.0f, 0.0f),
                                new Vector2(0.0f, 2.0f)
                            },
                            Duration = new List<float>() {
                                1.5f,
                                1.0f,
                                0.0f
                            },
                            VelocityLerpRate = 0.05f,
                        },
                    }
                });

                StageTime += 6.0f;
                stage.AddElement(StageTime, new CSpawnerEntity
                {
                    Type = typeof(CTurret),
                    SpawnCount = 3,
                    SpawnPosition = new CSpawnPositionFixed() { Position = new Vector2(450.0f, -100.0f) },
                    SpawnTimer = new CSpawnTimerInterval() { Delay = 1.0f },
                    CustomElement = new CSpawnerCustomMover() {
                        Mover = new CMoverSequence() {
                            Velocity = new List<Vector2>() {
                                new Vector2(0.0f, 2.0f),
                                new Vector2(2.0f, 0.0f),
                                new Vector2(0.0f, 2.0f)
                            },
                            Duration = new List<float>() {
                                1.5f,
                                1.0f,
                                0.0f
                            },
                            VelocityLerpRate = 0.05f,
                        },
                    }
                });

                // wave 1
                StageTime += 8.0f;
                stage.AddElement(StageTime, new CSpawnerEntity {
                    Type = typeof(CSinBall),
                    SpawnCount = 3,
                    SpawnPosition = new CSpawnPositionFixed() { Position = new Vector2(200.0f, -100.0f) },
                    SpawnTimer = new CSpawnTimerInterval() { Delay = 0.6f },
                    CustomElement = new CSpawnerCustomMover() { Mover = new CMoverFixedVelocity() { Velocity = new Vector2(0.0f, 2.5f) } },
                });

                StageTime += 4.0f;
                stage.AddElement(StageTime, new CSpawnerEntity {
                    Type = typeof(CSinBall),
                    SpawnCount = 3,
                    SpawnPosition = new CSpawnPositionFixed() { Position = new Vector2(400.0f, -100.0f) },
                    SpawnTimer = new CSpawnTimerInterval() { Delay = 0.6f },
                    CustomElement = new CSpawnerCustomMover() { Mover = new CMoverFixedVelocity() { Velocity = new Vector2(0.0f, 2.5f) } },
                });

                StageTime += 4.0f;
                stage.AddElement(StageTime, new CSpawnerEntity {
                    Type = typeof(CSinBall),
                    SpawnCount = 3,
                    SpawnPosition = new CSpawnPositionFixed() { Position = new Vector2(600.0f, -100.0f) },
                    SpawnTimer = new CSpawnTimerInterval() { Delay = 0.6f },
                    CustomElement = new CSpawnerCustomMover() { Mover = new CMoverFixedVelocity() { Velocity = new Vector2(0.0f, 2.5f) } },
                });

                // heavy asteroids
                StageTime += 6.0f;
                stage.AddElement(StageTime, new CSpawnerEntity {
                    Type = typeof(CAsteroid),
                    SpawnCount = 30,
                    SpawnPosition = new CSpawnPositionRandom(),
                    SpawnTimer = new CSpawnTimerRandom() { Frequency = 0.11f, IncreaseRate = 0.01f },
                    CustomElement = new CSpawnerCustomAsteroid(),
                });

                // wave 2
                StageTime += 8.0f;
                stage.AddElement(StageTime, new CSpawnerEntity {
                    Type = typeof(CPewPew),
                    SpawnCount = 4,
                    SpawnPosition = new CSpawnPositionFixed() { Position = new Vector2(200.0f, -100.0f) },
                    SpawnTimer = new CSpawnTimerInterval() { Delay = 0.8f },
                    CustomElement = new CSpawnerCustomMover() { Mover = new CMoverFixedVelocity() { Velocity = new Vector2(1.5f, 1.5f) } },
                });

                StageTime += 4.0f;
                stage.AddElement(StageTime, new CSpawnerEntity {
                    Type = typeof(CPewPew),
                    SpawnCount = 4,
                    SpawnPosition = new CSpawnPositionFixed() { Position = new Vector2(600.0f, -100.0f) },
                    SpawnTimer = new CSpawnTimerInterval() { Delay = 0.8f },
                    CustomElement = new CSpawnerCustomMover() { Mover = new CMoverFixedVelocity() { Velocity = new Vector2(-1.5f, 1.5f) } },
                });

                // heavy asteroids
                StageTime += 8.0f;
                stage.AddElement(StageTime, new CSpawnerEntity {
                    Type = typeof(CAsteroid),
                    SpawnCount = 30,
                    SpawnPosition = new CSpawnPositionRandom(),
                    SpawnTimer = new CSpawnTimerRandom() { Frequency = 0.11f, IncreaseRate = 0.01f },
                    CustomElement = new CSpawnerCustomAsteroid(),
                });

                // boss
                StageTime += 8.0f;
                stage.AddElement(StageTime, new CSpawnerEntity {
                    Type = typeof(CSinBall),
                    SpawnCount = 1,
                    SpawnPosition = new CSpawnPositionFixed() { Position = new Vector2(400.0f, -100.0f) },
                    SpawnTimer = new CSpawnTimerInterval() { Delay = 1.0f },
                    CustomElement = new CSpawnerCustomCode() { Code = SinBallBoss },
                });

                // stage end
                StageTime += 12.0f;
                stage.AddElement(StageTime, new CStageFinish());

                return stage;
            }

            public static void SinBallBoss(CEntity entity)
            {
                CSinBall boss = entity as CSinBall;

                boss.Health *= 15.0f;
                boss.BonusDrop = 30;
                boss.Visual.Scale *= 2.5f;
                boss.Mover = new CMoverSequence()
                {
                    Velocity = new List<Vector2>() {
                        new Vector2(0.0f, 1.0f),
                        new Vector2(0.0f, 0.0f),
                    },
                    Duration = new List<float>() {
                        3.5f,
                        0.0f,
                    },
                    VelocityLerpRate = 0.02f,
                };
                CollisionCircle collision = boss.Collision as CollisionCircle;
                collision.Radius *= 2.5f;
            }

        };
    }
}
             */
        }
    }
}
