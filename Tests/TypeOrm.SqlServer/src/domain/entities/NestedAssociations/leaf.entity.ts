import { Branch } from './branch.entity';
import { Entity, PrimaryGeneratedColumn, Column, ManyToOne } from 'typeorm';

@Entity('leaf')
export class Leaf {

  @PrimaryGeneratedColumn('uuid')
  id?: string;
  @Column()
  leafAttribute: string;

  @ManyToOne(() => Branch, (branch) => branch.leaves, { cascade: ['insert', 'update'], nullable: false, onDelete: 'CASCADE', orphanedRowAction: 'delete' })
  branch: Branch;

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}