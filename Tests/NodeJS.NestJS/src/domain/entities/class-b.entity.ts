import { ClassA } from './class-a.entity';
import { Entity, PrimaryGeneratedColumn, Column, ManyToOne } from 'typeorm';

@Entity('class_b')
export class ClassB {
  @PrimaryGeneratedColumn('uuid')
  id: string;

  @Column()
  attribute: string;

  @Column()
  classAId: string;

  @ManyToOne(() => ClassA, (classA) => classA.classBS, { cascade: ['insert', 'update'], nullable: false, onDelete: 'CASCADE', orphanedRowAction: 'delete' })
  classA: ClassA;

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}