using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetApp.Classes
{
    class VehicleDB
    {
        public static void AddVehicle(string name,Vehicle vehicle)
        {
            int months = 60;
            using (var connection = DbConnection.GetDbConnection())
            {
                try
                {
                    connection.Open();
                    try
                    {
                        var command = new SqlCommand("INSERT INTO TBL_VEHICLE_LOAN(FK_USERNAME,MODEL,MAKE,VEHICLE_PURCHASE_PRICE,VEHICLE_DEPOSIT," +
                            "VEHICLE_INTEREST,INSURANCE,VEHICLE_MONTHS_TO_PAY,VEHICLE_MONTHLY_PAYMENT,VEHICLE_TOTAL_PAYMENT) " +
                            "VALUES (@FK_USERNAME,@MODEL,@MAKE,@VEHICLE_PURCHASE_PRICE,@VEHICLE_DEPOSIT,@VEHICLE_INTEREST," +
                            "@INSURANCE,@VEHICLE_MONTHS_TO_PAY,@VEHICLE_MONTHLY_PAYMENT,@VEHICLE_TOTAL_PAYMENT)", connection);

                        command.Parameters.AddWithValue("@FK_USERNAME", name);
                        command.Parameters.AddWithValue("@MODEL", vehicle.model);
                        command.Parameters.AddWithValue("@MAKE", vehicle.make);
                        command.Parameters.AddWithValue("@VEHICLE_PURCHASE_PRICE", vehicle.purchasePrice);
                        command.Parameters.AddWithValue("@VEHICLE_DEPOSIT", vehicle.totalDeposit);
                        command.Parameters.AddWithValue("@VEHICLE_INTEREST", vehicle.interestRate);
                        command.Parameters.AddWithValue("@INSURANCE", vehicle.insurance);
                        command.Parameters.AddWithValue("@VEHICLE_MONTHS_TO_PAY", months);
                        command.Parameters.AddWithValue("@VEHICLE_MONTHLY_PAYMENT",vehicle.vehicleMonthlyPayment);
                        command.Parameters.AddWithValue("@VEHICLE_TOTAL_PAYMENT",vehicle.totalVehiclePayment);

                        command.ExecuteNonQuery();

                    }
                    catch (Exception ex) { throw ex; }
                    finally
                    {
                        connection.Close();
                    }
                }
                catch (Exception ex) { throw ex; }
                finally
                {
                    connection.Close();
                }
            }
        }
        public static bool FindVehicleUser(string username)
        {
            bool found = false;
            using (var connection = DbConnection.GetDbConnection())
            {
                try
                {
                    connection.Open();
                    try
                    {
                        var command = new SqlCommand(@"SELECT COUNT(*) FROM TBL_VEHICLE_LOAN WHERE (FK_USERNAME = @username)", connection);

                        command.Parameters.AddWithValue("@username", username);
                        int count = (int)command.ExecuteScalar();
                        if (count > 0)
                        {
                            found = true;
                        }
                    }
                    catch (Exception ex) { throw ex; }
                    finally
                    {
                        connection.Close();
                    }

                }
                catch (Exception ex) { throw ex; }
                finally
                {
                    connection.Close();
                }


            }
            return found;
        }
        public static Vehicle GetVehicleLoan(string name)
        {
            Vehicle vehicle = new Vehicle();
            using (var connection = DbConnection.GetDbConnection())
            {
                try
                {
                    connection.Open();
                    try
                    {
                        var command = new SqlCommand("SELECT MODEL,MAKE,VEHICLE_PURCHASE_PRICE,VEHICLE_DEPOSIT,VEHICLE_INTEREST," +
                                                     "INSURANCE,VEHICLE_MONTHS_TO_PAY,VEHICLE_MONTHLY_PAYMENT,VEHICLE_TOTAL_PAYMENT " +
                                                     "FROM TBL_VEHICLE_LOAN  WHERE (FK_USERNAME=@FK_USERNAME)", connection);

                        command.Parameters.AddWithValue("@FK_USERNAME", name);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                vehicle = new Vehicle
                                {
                                    model = reader["MODEL"].ToString(),
                                    make = reader["MAKE"].ToString(),
                                    purchasePrice = Convert.ToDecimal(reader["VEHICLE_PURCHASE_PRICE"]),
                                    totalDeposit = Convert.ToDecimal(reader["VEHICLE_DEPOSIT"]),
                                    interestRate = Convert.ToDecimal(reader["VEHICLE_INTEREST"]),
                                    insurance = Convert.ToDecimal(reader["INSURANCE"]),
                                    MONTHSTOPAY = Convert.ToInt32(reader["VEHICLE_MONTHS_TO_PAY"]),
                                    vehicleMonthlyPayment = Convert.ToDecimal(reader["VEHICLE_MONTHLY_PAYMENT"]),
                                    totalVehiclePayment = Convert.ToDecimal(reader["VEHICLE_TOTAL_PAYMENT"])
                                };
                            }
                        }


                    }
                    catch (Exception ex) { throw ex; }
                    finally
                    {
                        connection.Close();
                    }
                }
                catch (Exception ex) { throw ex; }
                finally
                {
                    connection.Close();
                }
            }

            return vehicle;
        }
    }
}
