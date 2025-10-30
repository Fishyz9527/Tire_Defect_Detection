using System.Windows.Forms;

namespace Tire_Defect_Detection
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.BtnRefresh = new System.Windows.Forms.Button();
            this.btnSimulateData = new System.Windows.Forms.Button();
            this.dgvMeasurements = new System.Windows.Forms.DataGridView();
            this.dgvStatistics = new System.Windows.Forms.DataGridView();
            this.lblStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMeasurements)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatistics)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.Location = new System.Drawing.Point(20, 20);
            this.BtnRefresh.Name = "BtnRefresh";
            this.BtnRefresh.Size = new System.Drawing.Size(80, 30);
            this.BtnRefresh.TabIndex = 0;
            this.BtnRefresh.Text = "刷新数据";
            this.BtnRefresh.UseVisualStyleBackColor = true;
            this.BtnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // btnSimulateData
            // 
            this.btnSimulateData.Location = new System.Drawing.Point(110, 20);
            this.btnSimulateData.Name = "btnSimulateData";
            this.btnSimulateData.Size = new System.Drawing.Size(80, 30);
            this.btnSimulateData.TabIndex = 1;
            this.btnSimulateData.Text = "模拟数据";
            this.btnSimulateData.UseVisualStyleBackColor = true;
            this.btnSimulateData.Click += new System.EventHandler(this.btnSimulateData_Click);
            // 
            // dgvMeasurements
            // 
            this.dgvMeasurements.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMeasurements.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMeasurements.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMeasurements.Location = new System.Drawing.Point(20, 60);
            this.dgvMeasurements.Name = "dgvMeasurements";
            this.dgvMeasurements.ReadOnly = true;
            this.dgvMeasurements.RowHeadersWidth = 82;
            this.dgvMeasurements.RowTemplate.Height = 37;
            this.dgvMeasurements.Size = new System.Drawing.Size(500, 400);
            this.dgvMeasurements.TabIndex = 2;
            // 
            // dgvStatistics
            // 
            this.dgvStatistics.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvStatistics.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvStatistics.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStatistics.Location = new System.Drawing.Point(600, 60);
            this.dgvStatistics.Name = "dgvStatistics";
            this.dgvStatistics.ReadOnly = true;
            this.dgvStatistics.RowHeadersWidth = 82;
            this.dgvStatistics.RowTemplate.Height = 37;
            this.dgvStatistics.Size = new System.Drawing.Size(500, 400);
            this.dgvStatistics.TabIndex = 3;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(200, 25);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(58, 24);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "就绪";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1187, 614);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.dgvStatistics);
            this.Controls.Add(this.dgvMeasurements);
            this.Controls.Add(this.btnSimulateData);
            this.Controls.Add(this.BtnRefresh);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "轮胎厚度检测监控系统";
            ((System.ComponentModel.ISupportInitialize)(this.dgvMeasurements)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatistics)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnRefresh;
        private System.Windows.Forms.Button btnSimulateData;
        private System.Windows.Forms.DataGridView dgvMeasurements;
        private System.Windows.Forms.DataGridView dgvStatistics;
        private System.Windows.Forms.Label lblStatus;
    }
}

